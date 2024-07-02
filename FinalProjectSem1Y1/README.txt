This game is the final project for year 1 semester 1 of C# basics in Tiltan.
(delivered late due to miluim)

This file is here to explain anything the game does not, including an
index for what symbols mean, the unique combat system, and how some enemies attack

Empty spaces and walls are very obvious, and don't need much explaining...
The Player is represented by a red ♦.
Basic enemies are green ♠.
The exit is marked with a blue X, and the entrance is a white E.
Chests are yellow ◘.
Ranged enemies do not move, and are represented by arrows indicating their attack direction ↑ ↓ ← →.
Traps are hidden until detected, and are represented by purple ¤ when visible.

The last 3 enemy types are represented by ╬, & and a surprise character which you will know when you see it if you get to the last level.
I am including a spoiler warning so you can play the game without knowing the enemies unique attack patterns before engaging with them,
but i will include them here because sadly I did not have enough time to create indicators in-game.

The Mini-Boss is immobile, and spots the player when he is in his line of sight (shares an X or Y parameter with him) and shoots if the player does not leave
that line of sight.

The heavy enemies pack a mean punch, and start charging they're attack when the player is in the AOE,
they hit on the next move, giving the player a small window to dodge.

                              X
They're AOE is as follows,  &XX
                              X

and will change depending on the attack's direction.

The Boss is a large enemy, and does not move for his first phase, it has a grace period where he "wakes up" and then starts his attack cycle.
When executing an attack, the boss will hit the player if they are in the direction of the attack, covering all the area in the map in his attack direction.

that is pretty much it, thank you for reading.