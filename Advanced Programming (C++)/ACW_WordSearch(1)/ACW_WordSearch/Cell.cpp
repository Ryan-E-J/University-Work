#include "Cell.h"
#include <string>
using namespace std;

Cell::Cell():
	completeWordStr(),
	letter(),
	completeWordBool()
{
}

const char Cell::letterReturn() const {
	return letter;
}

void Cell::addLetter(const char& NewLetter) {
	letter = NewLetter;
}

void Cell::contList(int test, const string& word) {
	bool exists = false;
	for (int i = 0; i < firstLetter.size(); i++)
	{
		if (word[test] == firstLetter[i].letterReturn()) {
			exists = true;
			test++;
			if (test > word.length())
			{
				completeWordBool = true;
				exists = true;
				completeWordStr = word;
				break;
			}
			else
			{
				firstLetter[i].contList(test, word);
				break;
			}
		}
	}
	if (!exists)
	{
		if (test + 1 > word.length())
		{
			completeWordBool = true;
			completeWordStr = word;
		}
		else
		{
			firstLetter.push_back(Cell());
			firstLetter[firstLetter.size() - 1].addLetter(word[test]);
			test++;
			firstLetter[firstLetter.size() - 1].contList(test, word);
		}
	}
}