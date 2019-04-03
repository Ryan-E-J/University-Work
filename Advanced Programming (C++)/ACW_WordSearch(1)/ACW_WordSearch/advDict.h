#pragma once
#include <string>
#include <vector>
#include "Cell.h"
using namespace std;

class advDict
{
	vector<Cell> firstLetter;
	int size;

public:
	explicit advDict();
	void ReadWord(const string& word);
	int Length() const;
	char Letter(const int& pos);
	void SizeChange(const int& gridSize);
	//string passBack();

private:

};

