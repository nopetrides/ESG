Welcome to a Rock, Paper Scissors assignment for East Side Games

You can find the link to project notes above under the "Projects tab" or a direct link to them here:
https://github.com/nopetrides/ESG/projects/1

No third party code was used, although the AudioManager is something I wrote a while before this project and modified it for this game.

The game was provided in a slightly broken and very barebones version. The first things I focused on was to cleanup the original project, finding and fixing any bugs in order to understand how the project was built.

There were some aspects of the project I could tell needed to be changed as they were done with intentionally bad c#/unity code practices. Hopefully I caught all of those in my review of the code. For example, using Get Component to save references to components is to be avoided. I also seperated out the game logic and the UI logic, following MVC pattern as best as was reasonable in a simple project like this.

Next I brainstormed feature development, including scheduling out what I thought was reasonable in the given time frame, 3 days. I didn't plan on spending 72 hours of development time, so I prioritized and thought of doable but scaleable code. Using this as an example of the kind of work I would do on a much bigger project, I wanted to try to refactor the game to be more akin to the bigger titles I have worked on. Therefore, my first and most complex task I wanted to attempt was to make the core mechanics data driven.

Of course, having data-driven RPS seemed like overkill, so I thought of the WHY RPS would be created with dynamic data - and so I formulated the RPS modes. The game currently includes the ability to play 3 modes:
Rock, Paper, Scissors (the classic)
Rock, Paper, Scissors, Lizard, Spock (A RPS with 5 choices, each having 2 winning and 2 losing hands)
and finally RPS-7 which is outlined on the following site: http://www.umop.com/rps7.htm

Buidling this system meant that you could create a rock paper scissors style game - sometimes known as RPS Variants or Advanced RPS - with any number of hand choices and winning/losing combinations.
Now technically, the number of choices should always be an odd number 3 or greater and be set so that each item has an equal nunmber of winning and losing hands - but technically that is not a requirement. If you tried to play a 6 choice varient with 1 choice that beats all others... well, I think only the computer will be happy to play with you.

After buiding out the base architectuure for the data driven gameplay, I went ahead and built the 3 game modes and all related data.

Next I started thinking about and implementing various "nice to haves" such an an sound system for auditory feedback on button presses. This was something I have done for game jams and the like so I customized an audio manager singleton I used in a previous project. This also gave me time to think on what I had so far and what else might be missing. I felt that there was a lack of visual feedback, and while I was instructed not to focus on UI, I couldn't help but feel that I should implement more visual representations for what is happening in the game, such as when you win or lose (there is no win or lose indicator, it simply modifies the player's score (or "money"). Doing this also helped kickstart my ideas for some new features.

So next I did some new features such as win streak, best streaks and highest money (i.e. score). Implementing the streak system as a multiplier to how much money you win for winning multiple times in a row, skyrocketed the pace that the player earned money. During testing I discovered that by spamming a single option, the player was hihgly likely to make a profit since the opponent chose randomly everytime while the player did not, and so any losses would be more than offset by the win streak multiplier. Therefore I wanted to add some "smarts" to the opponnent and prevent this kind of abuse. Of course, I also tried to make it so that the player could not abuse the computer system by forcing it into a pattern, so I kept elements of randomness in the opponents response pattern.

There were many other smaller changes and I have frequently commented the code so it should not be too difficult to undertand the direction the project could go in future. For the time constraints and initial simplicity of the project, I am quite content with it's current state and the amount of progress I was able to make.

Thanks for visiting!
