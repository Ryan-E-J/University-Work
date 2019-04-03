#include "WordSearch.h"
#include "advDict.h"
#include <fstream>
#include <iostream>
#include <string>
#include <vector>
using namespace std;


WordSearch::WordSearch(const char * const filename):
_advancedDict(new advDict()),
grid(),
dictionary(),
unfound(),
found(),
outputName(filename),
advGrid(),
xfound(),
yfound(),
dictVisit(),
gridVisit(),
depth(),
size(),
completeWord()
{
	// Add your code here
}

WordSearch::~WordSearch() {
	// Add your code here
	delete _advancedDict;
	delete[] advGrid;
}
//WordSearch::WordSearch(const WordSearch &name) {
//
//}

void WordSearch::ReadSimplePuzzle() {
	// Add your code here
	ifstream wordsearchtxt;
	wordsearchtxt.open(puzzleName);
	if (wordsearchtxt) 
	{
			wordsearchtxt >> size;
			_advancedDict->SizeChange(size);
			for (int i = 0; i < size; i++)
			{
				for (int f = 0; f < size; f++)
				{
					wordsearchtxt >> grid[i][f];
				}
			}
	}
	else
	{
		cout << "Could not find file wordsearch_grid.txt" << endl;
	}
}

void WordSearch::ReadSimpleDictionary() {
	// Add your code here
	ifstream wordsdict;
	wordsdict.open(dictionaryName);
	string contents;
	if (wordsdict)
	{
		while (getline(wordsdict, contents))
		{
			dictionary.push_back(contents);
		}
	}
	else
	{
		cout << "Could not find file dictionary.txt" << endl;
	}
}

void WordSearch::ReadAdvancedPuzzle() {
	// Add your code here
	ifstream wordsearchtxt;
	wordsearchtxt.open(puzzleName);
	if (wordsearchtxt)
	{
		wordsearchtxt >> size;
		advGrid = new char*[size];
		char* advPuzzleGrid = new char[size*size];
		for (int i = 0; i < size; i++)
		{
			advGrid[i] = advPuzzleGrid + size * i;
		}
		_advancedDict->SizeChange(size);
		for (int i = 0; i < size; i++)
		{
			for (int f = 0; f < size; f++)
			{
				wordsearchtxt >> advGrid[i][f];
			}
		}
	}
	else
	{
		cout << "Could not find file wordsearch_grid.txt" << endl;
	}
}

void WordSearch::ReadAdvancedDictionary() {
	// Add your code here
	ifstream wordsdict;
	wordsdict.open(dictionaryName);
	string contents;
	if (wordsdict)
	{
		while (getline(wordsdict, contents))
		{
			_advancedDict->ReadWord(contents);
		}
	}
	else
	{
		cout << "Could not find file dictionary.txt" << endl;
	}
}

void WordSearch::SolvePuzzleSimple() {
	// Add your code here
	//dictionary[][] first = line /// second = letter
	for (int i = 0; i < dictionary.size(); i++) //loop for every word in dict.
	{
		dictVisit++;
		for (int y = 0; y < size; y++) //loop for y axis
		{
			for (int x = 0; x < size; x++) //loop for x axis
			{
				gridVisit++;
				dictVisit++;
				if (grid[y][x] == dictionary[i][0]) //letter check
				{
					for (int j = 0; j < 8; j++)
					{
						int newY = y;
						int newX = x;
						newY += direction[j][0];
						newX += direction[j][1];
						gridVisit++;
						dictVisit++;
						if (!(0 < newY < 9 && 0 < newX < 9 && grid[newY][newX] != dictionary[i][1]))
						{
							for (int k = 2; k < dictionary[i].size(); k++)
							{
								gridVisit+=2;
								dictVisit+=2;
								newY += direction[j][0];
								newX += direction[j][1];
								if (grid[newY][newX] != dictionary[i][k])
								{
									goto incorrect;
								}
								if (k == dictionary[i].size() - 1 && grid[newY][newX] == dictionary[i][k])
								{
									found.push_back(dictionary[i]);
									xfound.push_back(x);
									yfound.push_back(y);
									goto complete;
								}
							}
						}
					incorrect:
						continue;
					}
				}
			}
		}
		dictVisit++;
		unfound.push_back(dictionary[i]);
	complete:
		continue;
	}
}

void WordSearch::SolvePuzzleAdvanced() {
	// Add your code here
	for (int y = 0; y < size; y++)
	{
		for (int x = 0; x < size; x++)
		{
			for (int i = 0; i < _advancedDict->Length(); i++)
			{
				completeWord = false;
				if (advGrid[y][x] == _advancedDict->Letter(i))
				{
					for (int j = 0; j < 8; j++)
					{
						depth = 1;
						int newY = y;
						int newX = x;
						newY += direction[j][0];
						newX += direction[j][1];
					}
				}
			}
		}
	}
}

void WordSearch::WriteResults(const double loadTime, const double solveTime) const {
	// Add your code here
	ofstream output;
	output.open(outputName);
	output << "NUMBER_OF_WORDS_MATCHED " << dictionary.size() - unfound.size() << endl;
	output << "WORDS_MATCHED_IN_GRID" << endl;
	for (int i = 0; i < found.size(); i++)
	{
		output << xfound[i] << " " << yfound[i] << " " << found[i] << endl;
	}
	output << "WORDS_UNMATCHED_IN_GRID" << endl;
	for (int i = 0; i < unfound.size(); i++)
	{
		output << unfound[i] << endl;
	}
	output << "NUMBER_OF_GRID_CELLS_VISITED " << gridVisit << endl;
	output << "NUMBER_OF_DICTIONARY_ENTERIES_VISITED " << dictVisit << endl;
	output << "TIME_TO_POPULATE_GRID_STRUCTURE " << loadTime << endl;
	output << "TIME_TO_SOLVE_PUZZLE " << solveTime << endl;
}