#ifndef _JS_BUILDER_H_
#define _JS_BUILDER_H_

#include <fstream>

using namespace std;

#ifndef _DISABLE_BOOST
#include <boost/filesystem.hpp>
#include <boost/system/error_code.hpp>
#include <boost/lexical_cast.hpp>
#include <boost/iostreams/filtering_stream.hpp>
#include <boost/iostreams/filter/gzip.hpp>
#include <boost/iostreams/copy.hpp>
#include <boost/archive/iterators/base64_from_binary.hpp>
#include <boost/archive/iterators/insert_linebreaks.hpp>
#include <boost/archive/iterators/transform_width.hpp>
#include <boost/archive/iterators/ostream_iterator.hpp>
#include <boost/uuid/uuid.hpp>
#include <boost/uuid/uuid_generators.hpp>
#include <boost/uuid/uuid_io.hpp>

using namespace boost::iostreams;

namespace fs = boost::filesystem;
namespace its = boost::archive::iterators;

/**************************************************************************************************/
typedef its::base64_from_binary<its::transform_width<const char *, 6, 8>> base64_text;
#else
#include <string>
#include <sstream>
#include <stdexcept>
#include "zlib.h"

using std::string;
using std::stringstream;
#pragma comment(lib, "zlibstatic")
#endif

/**************************************************************************************************/
const size_t JSON_CHUNK_MAX_SIZE = 10 * 1024;

/**************************************************************************************************
string => gzip => base64
*/

#ifdef _DISABLE_BOOST
// https://blog.cppse.nl/deflate-and-gzip-compress-and-decompress-functions

// Found these here http ://mail-archives.apache.org/mod_mbox/trafficserver-dev/201110.mbox/%3CCACJPjhYf=+br1W39vyazP=ix
//eQZ-4Gh9-U6TtiEdReG3S4ZZng@mail.gmail.com%3E
#define MOD_GZIP_ZLIB_WINDOWSIZE 15
#define MOD_GZIP_ZLIB_CFACTOR    9
#define MOD_GZIP_ZLIB_BSIZE      8096

// Found this one here: http://panthema.net/2007/0328-ZLibString.html, author is Timo Bingmann
// edited version
/** Compress a STL string using zlib with given compression level and return
  * the binary data. */
std::string compress_gzip(const std::string& str,
	int compressionlevel = Z_BEST_COMPRESSION)
{
	z_stream zs;                        // z_stream is zlib's control structure
	memset(&zs, 0, sizeof(zs));

	if (deflateInit2(&zs,
		compressionlevel,
		Z_DEFLATED,
		MOD_GZIP_ZLIB_WINDOWSIZE + 16,
		MOD_GZIP_ZLIB_CFACTOR,
		Z_DEFAULT_STRATEGY) != Z_OK
		) {
		throw(std::runtime_error("deflateInit2 failed while compressing."));
	}

	zs.next_in = (Bytef*)str.data();
	zs.avail_in = str.size();           // set the z_stream's input

	int ret;
	char outbuffer[32768];
	std::string outstring;

	// retrieve the compressed bytes blockwise
	do {
		zs.next_out = reinterpret_cast<Bytef*>(outbuffer);
		zs.avail_out = sizeof(outbuffer);

		ret = deflate(&zs, Z_FINISH);

		if (outstring.size() < zs.total_out) {
			// append the block to the output string
			outstring.append(outbuffer,
				zs.total_out - outstring.size());
		}
	} while (ret == Z_OK);

	deflateEnd(&zs);

	if (ret != Z_STREAM_END) {          // an error occurred that was not EOF
		std::ostringstream oss;
		oss << "Exception during zlib compression: (" << ret << ") " << zs.msg;
		throw(std::runtime_error(oss.str()));
	}

	return outstring;
}

// https://renenyffenegger.ch/notes/development/Base64/Encoding-and-decoding-base-64-with-cpp
static const std::string base64_chars =
"ABCDEFGHIJKLMNOPQRSTUVWXYZ"
"abcdefghijklmnopqrstuvwxyz"
"0123456789+/";


static inline bool is_base64(unsigned char c) {
	return (isalnum(c) || (c == '+') || (c == '/'));
}

std::string base64_encode(unsigned char const* bytes_to_encode, unsigned int in_len) {
	std::string ret;
	int i = 0;
	int j = 0;
	unsigned char char_array_3[3];
	unsigned char char_array_4[4];

	while (in_len--) {
		char_array_3[i++] = *(bytes_to_encode++);
		if (i == 3) {
			char_array_4[0] = (char_array_3[0] & 0xfc) >> 2;
			char_array_4[1] = ((char_array_3[0] & 0x03) << 4) + ((char_array_3[1] & 0xf0) >> 4);
			char_array_4[2] = ((char_array_3[1] & 0x0f) << 2) + ((char_array_3[2] & 0xc0) >> 6);
			char_array_4[3] = char_array_3[2] & 0x3f;

			for (i = 0; (i < 4); i++)
				ret += base64_chars[char_array_4[i]];
			i = 0;
		}
	}

	if (i)
	{
		for (j = i; j < 3; j++)
			char_array_3[j] = '\0';

		char_array_4[0] = (char_array_3[0] & 0xfc) >> 2;
		char_array_4[1] = ((char_array_3[0] & 0x03) << 4) + ((char_array_3[1] & 0xf0) >> 4);
		char_array_4[2] = ((char_array_3[1] & 0x0f) << 2) + ((char_array_3[2] & 0xc0) >> 6);

		for (j = 0; (j < i + 1); j++)
			ret += base64_chars[char_array_4[j]];

		while ((i++ < 3))
			ret += '=';

	}

	return ret;

}

std::string base64_decode(std::string const& encoded_string) {
	int in_len = encoded_string.size();
	int i = 0;
	int j = 0;
	int in_ = 0;
	unsigned char char_array_4[4], char_array_3[3];
	std::string ret;

	while (in_len-- && (encoded_string[in_] != '=') && is_base64(encoded_string[in_])) {
		char_array_4[i++] = encoded_string[in_]; in_++;
		if (i == 4) {
			for (i = 0; i < 4; i++)
				char_array_4[i] = base64_chars.find(char_array_4[i]);

			char_array_3[0] = (char_array_4[0] << 2) + ((char_array_4[1] & 0x30) >> 4);
			char_array_3[1] = ((char_array_4[1] & 0xf) << 4) + ((char_array_4[2] & 0x3c) >> 2);
			char_array_3[2] = ((char_array_4[2] & 0x3) << 6) + char_array_4[3];

			for (i = 0; (i < 3); i++)
				ret += char_array_3[i];
			i = 0;
		}
	}

	if (i) {
		for (j = 0; j < i; j++)
			char_array_4[j] = base64_chars.find(char_array_4[j]);

		char_array_3[0] = (char_array_4[0] << 2) + ((char_array_4[1] & 0x30) >> 4);
		char_array_3[1] = ((char_array_4[1] & 0xf) << 4) + ((char_array_4[2] & 0x3c) >> 2);

		for (j = 0; (j < i - 1); j++) ret += char_array_3[j];
	}

	return ret;
}
#endif // !_DISABLE_BOOST

string gZipEncode(const string & strText)
{
#ifndef _DISABLE_BOOST
	filtering_streambuf<input> gzipCompressor;
	gzipCompressor.push(gzip_compressor());
	gzipCompressor.push(boost::iostreams::array_source(strText.data(), strText.size()));

	std::stringstream gzipOutputStream;
	boost::iostreams::copy(gzipCompressor, gzipOutputStream);

	string strGzipText = gzipOutputStream.str();

	std::stringstream streamBase64;
	std::copy(base64_text(strGzipText.c_str()), base64_text(strGzipText.c_str() + strGzipText.size()), ostream_iterator<char>(streamBase64));

	return streamBase64.str();
#else
	string strGzipText = compress_gzip(strText);

	string strBase64Text = base64_encode(reinterpret_cast<const unsigned char*>(strGzipText.c_str()), strGzipText.length());

	return strBase64Text;
#endif
}

/**************************************************************************************************
declares 'json' var (one or multiple lines); the input JSON is compresses with gzip and base64 encoded 

I - one line:
var json = gZipDecode('H4sIAAAAAAAA/93WwQoCIRAG4FcZ9rwO46w62qtEp9hD14ou0btnCxsutCAd');

II - multiple lines
var json = '';
json += gZipDecode('H4sIAAAAAAAA/02avZrkMHIEX2W/s88gCIIg9CoyTzLOkSNTL6+JyAJ2jGkgYwfZPb');
json += gZipDecode('H4sIAAAAAAAA/03au7HtsBVEwVQUgIwH4p9/YtLtIQ7hwOiqPd7yUP77n/I8j7d6/1');
json += gZipDecode('H4sIAAAAAAAA/03aMbbFKBJEwa3MgIwH4p9/YtLtIQ7hwOiqPd7yUP77n/I8j7d6/1');

Example input:
{ "indices" : [1, 22, 44, 66, 23, 88] }

Usage:
triangles.indices = JSON.parse(json).indices;

Notes:
JSON_CHUNK_MAX_SIZE avoids Stack overflow error.
*/
string JSON2GZipBase64JS(const string& strJSON)
{
	string strJS;

	if (strJSON.size() > JSON_CHUNK_MAX_SIZE)
	{
		strJS = "var json = '';\n";
		size_t iChunkStartPos = 0;
		while (iChunkStartPos < strJSON.size())
		{
			size_t iChunkSize = JSON_CHUNK_MAX_SIZE;
			if ((iChunkStartPos + JSON_CHUNK_MAX_SIZE) > strJSON.size())
			{
				iChunkSize = strJSON.size() - iChunkStartPos;
			}

			string strJSONChunk = strJSON.substr(iChunkStartPos, iChunkSize);
			strJS += "json += gZipDecode('";
			strJS += gZipEncode(strJSONChunk);
			strJS += "');\n";

			iChunkStartPos += JSON_CHUNK_MAX_SIZE;
		} // while (iChunkStartPos < ...
	}
	else
	{
		strJS += "var json = gZipDecode('"; 
		strJS += gZipEncode(strJSON);
		strJS += "');\n";
	}

	return strJS;
}

#endif // _JS_BUILDER_H_