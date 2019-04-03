#pragma once
#include "advDict.h"
#include <string>
#include <vector>
class WordSearch {
	const char* puzzleName = "wordsearch_grid.txt";
	const char* dictionaryName = "dictionary.txt";

	// Add your code here
	advDict* _advancedDict;
	const int direction[8][2] = {
		{ -1,0 },//up
		{ -1,1 },//up right
		{ 0,1 },//right
		{ 1,1 },//down right
		{ 1,0 },//down
		{ 1,-1 },//down left
		{ 0,-1 },//left
		{ -1,-1 }//up left
	};
	char grid[9][9] = {};
	std::vector<std::string> dictionary;
	std::vector<std::string> unfound;
	std::vector<std::string> found;
	std::string const outputName;
	char** advGrid;
	std::vector<int> xfound;
	std::vector<int> yfound;
	int dictVisit = 0;
	int gridVisit = 0;
	int depth;
	int size;
	bool completeWord;
	WordSearch(const WordSearch &name) = delete;


public:
	explicit WordSearch(const char * const filename);
	~WordSearch();
	WordSearch & operator= (WordSearch & obj);

	void ReadSimplePuzzle();
	void ReadSimpleDictionary();
	void ReadAdvancedPuzzle();
	void ReadAdvancedDictionary();
	void SolvePuzzleSimple();
	void SolvePuzzleAdvanced();
	void WriteResults(const	double loadTime, const double solveTime) const;

	// Add your code here
};

