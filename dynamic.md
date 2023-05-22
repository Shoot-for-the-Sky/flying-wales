# Dynamic Elements

## Numerical values

The numerical value in the game are:

1. `Health Points` of whales and enemies - The health point of each whale alone will be `100` and will stay constant during game, for making it balanced with the game that becoming more difficult and challenging as more we play throw the scenarios, there will be more whales over time.

2. `Damage Points` of an enemy attack - The damage point will by dynamically changing from different enemies between `50` to `200`, in each scenario we have different kind of enemies when each has different strategy if attacking.

3. `Score` of player (Gathering during play) - The score will change dynamically during the scenarios and will be increased over time, the score will be gathering by different operations like: collecting good elements from the map, survive, attacking enemies, solving checklist challenges tasks, each operation will have his own score from scale of `1` to `50`

4. `Number Of Whales` Increasing during game - The number of whales is changing overtime in the game and the player have the ability to choose if to call to a new whale or not by "paying" scores, the number of whales in the beginning will be `3` and can be maximum `20`, this is because when we play with over `20` whales it become very difficult to control them.

## Object Positions

1. `Whales` - the position of the whales it by the player control.
2. `Enemies` - we have two different types of enemies and each has its own positioning:
   1. `Static Enemy` - moving randomly in the screen, the `position` and `direction` is chosen randomly but the `speed` is changing by the type of the enemy, for example: meteors, missiles, bombs etc.
   2. `Dynamic Enemy` - the position is changing by dynamic behavior of the enemy, it can change its position by reaching specific health points or time, for example: aliens, humans etc.
3. `Goods` - the goods that are on the map are positioning randomly in the screen, each scenario has its own type of goods and have different time between each good is generated.

## Object Behaviours

1. `Whales` - the player has 3 main state to control them and each has its own way of moving whales:
   1. `Dynamic` - the whales are moving randomly around the map and searching for goods.
   2. `Track` - the whale are track the player mouse.
   3. `Attack` - the whales will attack the position the player have clicked.
2. `Enemies` - enemies behaviour changing dynamically to the game status, for example the `static enemey` meteor will appear and operate in specific times of the scenario and will come as "waves", the `dynamic enemey` like aliens, will behave dynamically to the whales status and the scenario status.

## Economic System

The economic system on this game based on the player scores, and during the game he can spend it buying shield to the whales or calling for "help" and one more whale will join the whales group.
The way the player earn this score during game is by - surviving with no hits for specific time, destroying enemies, collect using `dynamic` whale state etc.

## Player Information

The player information he can see during game are:
- The number of current whales
- The score value of the player
- Which whale state is in
- What is the current and next check list tasks he needed to perform
- Where from (direction) the enemy are coming from

## Player Control

The player control direct to the whales during game in real-time.
The controlling methods are:

- Changing whales states
- Controlling whales positions using mouse

## Player Strategies

The player strategies is choosing which way to control the whales and how to spend the score to defend them, the main goal of the player is to perform all the checklist tasks of a scenario.