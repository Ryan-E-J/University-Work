#pragma once
#include <vector>
using namespace std;

class Cell
{
	vector<Cell> firstLetter;
	string completeWordStr;
	char letter;
	bool completeWordBool;

public:
	explicit Cell();
	const char letterReturn() const;
	void addLetter(const char& firstLetter);
	void contList(int test, const string& word);

private:

};