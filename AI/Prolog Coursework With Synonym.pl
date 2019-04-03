det(a).
det(an).
det(the).

noun(boy).
noun(girl).
noun(man).
noun(woman).
noun(grandma).
noun(grandmother).
noun(grandad).
noun(grandfather).
noun(student).
noun(father).
noun(person).
noun(grasndfather).

adj(young).
adj(old).
adj(fat).
adj(skinny).
adj(tall).
adj(short).
adj(smart).
adj(technical).
adj(small).
adj(teenage).
adj(lonely).
adj(good).
adj(manual).
adj(elderly).
adj(very).

verb(love).
verb(loves).
verb(likes).
verb(thrives).
verb(adore).
verb(adores).

noun(problem).
noun(challenge).
noun(book).
noun(conversation).
noun(golf).
noun(party).

prep(with).

present(construction_kit,subject(very,young,boy),verb(loves),object(manual,problem)):-
    nl,write("Gift Idea: "),write("Construction Kit").
present(money,subject(teenage,student),verb(loves),object(party)):-
        nl,write("Gift Idea: "),write("Money").
present(raspberry_pi,subject(teenage,girl),verb(loves),object(challenge)):-
        nl,write("Gift Idea: "),write("Raspberry Pi").
present(brief_history_of_time,subject(elderly,grandfather),verb(likes),object(good,book)):-
        nl,write("Gift Idea: "),write("The book: Brief History of Time").
present(brief_history_of_time,subject(elderly,grasndfather),verb(likes),object(good,book)):-
        nl,write("Gift Idea: "),write("The book: Brief History of Time").
present(the_internet,subject(lonely,person),verb(thrives),object(conversation)):-
        nl,write("Gift Idea: "),write("The Internet").
present(golfing_sweater,subject(young,father),verb(loves),object(golf)):-
        nl,write("Gift Idea: "),write("Golfing Sweater").
present("Nothing Found",_,_,_).

/*The agent section is the part that is called at the start of the program
from here the user will be asked to input the details of the person
the agent will then send this perception of the world onto the action
phase*/
agent:-
    percieve(Percepts),
    action(Percepts).

/*The percieve section is where the program will percieve the world around it
in this case that would be taking an input from the user.*/
percieve(Percepts):-
    write("Please enter a description of the person:"),
    read(Percepts).

/*The action section is like a guidepost for where the items should go
first sending of the perception of the world to sentence which will hand back the parsed sentence.
This sentence will then proceed to be parsed further through retrieveoutput, the results of the retrieveoutput parse however come back only as lists, so they are then sent to their corresping section to become linked to Subject, verb or Object depending on which list it is.
After this these 3 variables are sent into the whole looping process to check for their synonyms, for each synonym there will be a present check, and one at the end so it doesn't miss those without synonyms.*/
action(Percepts):-
    sentence(Percepts, O),
    listsGet(O, Subj, V, Obj),
    subjectlist(Subj, Subject),
    objectlist(Obj, Object),
    syn(V, SynList),
    append(V,SynList,SynComp),
    loopPresent(Subject, Object, SynComp,Obj).

/*This loops through the synonyms for verb phrase, then proceeds onto looking at the noun synonyms, after that it checks present again in case something didn't work*/
loopPresent(_,_,[],_).
loopPresent(Subject, Object, [Head|Tail],Obj):-
    verblist([Head], Verb),
    nounPreLoop(Subject, Object, Verb,Obj),
    loopPresent(Subject,Object,Tail,Obj),
    present(X,Subject,Verb,Object).

/*This readies the Synonym list for the noun loops. There are two loops due to the different positions for each sentence.*/
nounPreLoop(Subject, Object, Verb,[H|T]):-
    syn([H], Synonym),
    nounLoopTail(Subject, Object, Verb, Synonym, T),
    syn(T, Syn),
    nounLoopHead(Subject,Object,Verb,Syn,H).

/*This will loop through all the synonyms and check them against present. This one is for the sentences with synonyms at the end of verb phases' nouns*/
nounLoopHead(_,_,_,[],_).
nounLoopHead(Subject,Object,Verb,[H|T],NounHead):-
    append([NounHead],[H], Noun2),
    objectlist(Noun2, Test2),
    present(X,Subject,Verb,Test2),
    nounLoopHead(Subject,Object,Verb,T,NounHead).

/*This will loop through all the synonyms and check them against present. This one is for the sentences with synonyms on their own in the verb phases' nouns*/
nounLoopTail(_,_,_,[],_).
nounLoopTail(Subject,Object,Verb,[H|T], NounTail):-
    append([H],NounTail, Noun),
    objectlist(Noun, Test),
    present(X,Subject,Verb,Test),
    nounLoopTail(Subject,Object,Verb,T,NounTail).

syn([loves|_],[likes,adores]).
syn([likes|_],[loves,adores]).
syn([adore|_],[likes,loves]).
syn([challenge|_],[problem,challenge]).
syn([problem|_],[challenge,problem]).
syn(_,[]).

/*This method will recieve the setence parse and proceed to parse it, this one is used if there is a prepositional or a determiner in front of the noun section in the verb phase*/
listsGet(sentence(np(np(_,NP)),vp(vp(V,pp(_,np(_,NP2))))),Subject,Verb,Object):-
    get_np2(NP,Subject),
    get_verb(V,Verb),
    get_np2(NP2,Object).

/*This method is the same overall as the one above however is for sentences thathave 1 determiner or a prepositional before the noun in verb phase*/
listsGet(sentence(np(np(_,NP)),vp(vp(V,np(_,NP2)))),Subject,Verb,Object):-
    get_np2(NP,Subject),
    get_verb(V,Verb),
    get_np2(NP2,Object).
/*This method is once again the same as the above method however this is for sentences that do not have anything in between the verb and the noun in verb phrase.*/
listsGet(sentence(np(np(_,NP)),vp(vp(V,NP2))), Subject, Verb, Object):-
    get_np2(NP,Subject),
    get_verb(V,Verb),
    get_np2(NP2,Object).

/*get_np2 is used to take out the individual adjectives before a noun, or just take the noun on it's own and put it into a list as requested by present (however this does not complete it in the way that is required by present, this is done at a later stage.*/
get_np2(np2(adj(Adj1),np2(adj(Adj2),np2(noun(Noun1)))),[Adj1,Adj2,Noun1]).
get_np2(np2(adj(Adj1),np2(noun(Noun1))),[Adj1,Noun1]).
get_np2(np2(noun(Noun1)),[Noun1]).
get_verb(verb(Verb1),[Verb1]).
/*get_verb takes the verb from the verb phase and places it inside of a list for the same reason as the nounphase sections*/

subjectlist([A,B,C],subject(A,B,C)).
subjectlist([A,B],subject(A,B)).
/*subjectlist will take the list that was given from retrieve output an split them into the format required by the present rule which is as follows "subject(Adj,Adj,Noun)" or less depending on the sentence as shown by the second subject method.*/

verblist([A],verb(A)).
/*This takes the verb that was parsed and places it into a verb bracket*/

objectlist([A,B],object(A,B)).
objectlist([A],object(A)).
/*This takes the object from the verb phase and places them into object brackets, there are two for each size.*/

sentence(Sentence,sentence(np(Noun_Phrase),vp(Verb_Phrase))):-
	np(Sentence,Noun_Phrase,Rem),
	vp(Rem,Verb_Phrase).
/*Sentence is the start of the parse for the input, it is first sent to nounphase then to verbphase as that's the correct format of a sentence*/

np([X|T],np(det(X),NP2),Rem):-
	det(X),
	np2(T,NP2,Rem).
np(Sentence,Parse,Rem):- np2(Sentence,Parse,Rem).
np(Sentence,np(NP,PP),Rem):-
	np(Sentence,NP,Rem1),
	pp(Rem1,PP,Rem).
/*Noun phase will take in the start of a sentence where it will first check for a determiner, if nothing it will check for an noun by passing it to NP2, if that fails it checks if it is an adjective. The last section is to check for a prepositional of a sentence if none of the others succeed in finding a correct rule.*/

np2([H|T],np2(noun(H)),T):- noun(H).
np2([H|T],np2(adj(H),Rest),Rem):- adj(H),np2(T,Rest,Rem).

pp([H|T],pp(prep(H),Parse),Rem):-
	prep(H),
	np(T,Parse,Rem).

vp([H|[]],verb(H)):- verb(H).
vp([H|Rest],vp(verb(H),RestParsed)):-
	verb(H),
	pp(Rest,RestParsed,_).
vp([H|Rest],vp(verb(H),RestParsed)):-
    verb(H),
    np(Rest,RestParsed,_).
/*Verb phase will be handed the leftovers of the sentencefrom nounphase, once given it vp will test if the first word is a verb, then proceed to check if it is a prepositional, then check if it is a noun.*/
