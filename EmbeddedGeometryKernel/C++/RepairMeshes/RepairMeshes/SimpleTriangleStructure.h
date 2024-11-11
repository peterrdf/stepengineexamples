#pragma once


struct SIMPLE_POINT
{
	double			x;
	double			y;
	double			z;
};

struct SIMPLE_TRIANGLE
{
	SIMPLE_POINT	pntI;
	SIMPLE_POINT	pntII;
	SIMPLE_POINT	pntIII;

	SIMPLE_TRIANGLE	* next;
};
