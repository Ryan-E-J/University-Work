#include "advDict.h"
#include <string>
#include <iostream>
using namespace std;

advDict::advDict():
size()
{
}

void advDict::ReadWord(const string& word)
{
	bool exists = false;
	for (int i = 0; i < firstLetter.size(); i++)
	{
		if (word[0] == firstLetter[i].letterReturn()) {
			exists = true;
			int test;
			test = 1;
			firstLetter[i].contList(test, word);
			break;
		}
	}
	if (!exists)
	{
		int test;
		test = 1;
		firstLetter.push_back(Cell());
		firstLetter[firstLetter.size() - 1].addLetter(word[0]);
		firstLetter[firstLetter.size() - 1].contList(test, word);
	}
}

int advDict::Length() const {
	return firstLetter.size();
}

char advDict::Letter(const int& pos) {
	return firstLetter[pos].letterReturn();
}

void advDict::SizeChange(const int& gridSize) {
	size = gridSize;
}

//string advDict::passBack() {

//}