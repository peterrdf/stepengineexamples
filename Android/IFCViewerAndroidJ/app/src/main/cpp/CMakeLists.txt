cmake_minimum_required(VERSION 3.4.1)

# now build app's shared lib
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11 -Wall")

# configure import libs
set(distribution_DIR ${CMAKE_CURRENT_SOURCE_DIR}/../../../../distribution)

# shared lib will also be tucked into APK and sent to target
# refer to app/build.gradle, jniLibs section for that purpose.
# ${ANDROID_ABI} is handy for our purpose here. Probably this ${ANDROID_ABI} is
# the most valuable thing of this sample, the rest are pretty much normal cmake
add_library(lib_ifcengine SHARED IMPORTED)
set_target_properties(lib_ifcengine PROPERTIES IMPORTED_LOCATION
        ${distribution_DIR}/ifcengine/lib/${ANDROID_ABI}/libifcengine.so)

add_library(gl2jni SHARED
        ifcenginewrapper.cpp
        IFCModel.cpp
        gl_code.cpp)

target_include_directories(gl2jni PRIVATE
        ${distribution_DIR}/ifcengine/include)

# add lib dependencies
target_link_libraries(gl2jni
        android
        log
        EGL
        GLESv2
        lib_ifcengine)

