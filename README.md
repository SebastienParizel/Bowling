# Bowling
## Bowling rules
A game of bowling consists of ten frames.
In each frame, the bowler will have two chances to knock down as many pins as possible with their bowling ball. If a bowler is able to knock down all ten pins with their first ball, he is awarded a strike. If the bowler is able to knock down all 10 pins with the two balls of a frame, it is known as a spare. Bonus points are awarded for both of these, depending on what is scored in the next 2 balls (for a strike) or 1 ball (for a spare).
If the bowler knocks down all 10 pins in the tenth frame, the bowler is allowed to throw 3 balls for that frame.
This allows for a potential of 12 strikes in a single game, and a maximum score of 300 points, a perfect game.
## Installation
todo
## Running application
The application will provide the current score and, for each frame, the intermediate score, the specificity of the frame
```
Bowling.exe 1 2 3 4 5 5 1 0 10 0 3 3
```
The following command will get the following result:
```
Final score: 44
-----------------------
* Frame n°1
        intermediate score: 3
        Is strike: False
        Is spare:False
* Frame n°2
        intermediate score: 10
        Is strike: False
        Is spare:False
* Frame n°3
        intermediate score: 21
        Is strike: False
        Is spare:True
* Frame n°4
        intermediate score: 22
        Is strike: False
        Is spare:False
* Frame n°5
        intermediate score: 38
        Is strike: True
        Is spare:False
* Frame n°6
        intermediate score: 44
        Is strike: False
        Is spare:False
```
It is also possible to use specific character for special case:
- No pin dropper : -
- Strike : X
- Spare : /
In the previous example, you can get this
```
Bowling.exe 1 2 3 4 5 / 1 - X - 3 3
```