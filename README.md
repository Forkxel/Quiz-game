# Quiz game

Quiz game done in Windows Forms with loading questions from the database.

## Features

1. four different categories and category for mix of all questions
2. three different difficulties
3. Make profile to save highscore

## Getting Started

### Dependencies

<ul>
    <li>.NET 8.0 SDK</li>
    <li>SQL Database for data storage</li>
    <li>
        NuGet packages:
        <ul>
            <li>System.Data.SqlClient</li>
            <li>System.Configuration.ConfigurationManager</li>
        </ul>
    </li>
</ul>

### Installing

1. Open Visual Studio.
2. Click Clone a repository.
3. Paste https://github.com/Forkxel/Quiz-game.git to Repository location.
4. In Solution Explorer find App.config and open it.
5. In the file you will find
<ul>
    <ul>
        <li>DataSource - Enter name of your server.</li>
        <li>Database - Enter name of your database.</li>
        <li>Name - Your user name.</li>
        <li>Password - Your password.</li>
    </ul>
</ul>

```
<add key="DataSource" value=""/>
<add key="Database" value=""/>
<add key="Name" value=""/>
<add key="Password" value=""/>
```
6. Open query.sql
7. Enter the name of your database and run the query
```
use <your-database-name>;
```
8. Install NuGet packages if needed.
9. Run the application.

### Executing program

After you run the program first thing you will se is main form with:

<ol>
    <li><a href="#1-profile-button">Profile button</a></li>
    <li><a href="#2-score-board-button">Score board button</a></li>
    <li><a href="#3-category-combobox">Category ComboBox</a></li>
    <li><a href="#4-difficulty-combobox">Difficulty ComboBox</a></li>
    <li><a href="#5-start-button">Start button</a></li>
    <li><a href="#6-type-of-questions">Type of questions</a></li>
    <li><a href="#7-play-again">Play again</a></li>
</ol>
 
#### 1. Profile button

After clicking this button new form will be shown with text boxes to enter username and password. After writting your details you can log in or you can sign up and make a new profile. After successfull log in or sign up the form will change. New label with the username of the logged in user will be shown and two buttons for sign out and change password. If user want to change the password he can't change the password to same password as he already has.

#### 2. Score board button

Will show new form with the score of the top 5 users.

#### 3. Category ComboBox

You can choose out of these categories
<ul>
    <li>Geography</li>
    <li>History</li>
    <li>Biology</li>
    <li>Films</li>
    <li>Mixed</li>
</ul>

#### 4. Difficulty ComboBox

You can choose out of these difficulties
<ul>
    <li>Easy</li>
    <li>Medium</li>
    <li>Hard</li>
</ul>

#### 5. Start button

After clicking this button the game will start you need to answer on 6 questions on every question you will have 30 seconds after clicking submit button the correct answer will reveal and the button will change to next button.

#### 6. Type of questions

There are 4 types of questions
<ul>
    <li>Single choice question</li>
    <li>Multiple choice question</li>
    <li>True or false question</li>
    <li>Written answer question</li>
</ul>

#### 7. Play again button

Resets score and form to selection of category and difficulty. User stay logged in after clicking this button.

## Customization

You can add your custom questions using the query.sql 

## Help

If you do not use MSSQL server, this app might not work.

If you need anything from me about this application contact me at:
* pavel.halik06@gmail.com
