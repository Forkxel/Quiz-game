use halik;

create table Category(
	id int primary key identity(1,1),
	nameCategory varchar(50)
);

create table Difficulty(
	id int primary key identity(1,1),
	nameDifficulty varchar(50)
);

create table Questions(
	id int primary key identity(1,1),
	questionText varchar(max),
	correctAnswer varchar(max),
	option1 varchar(max),
	option2 varchar(max),
	option3 varchar(max),
	diff_id int foreign key(diff_id) references Difficulty(id),
	cat_id int foreign key(cat_id) references Category(id)
);

insert into Difficulty(nameDifficulty) values ('Easy'),('Medium'),('Hard');

insert into Category(nameCategory) values ('Geography'), ('History'), ('Biology'), ('Films');

insert into Questions (questionText, correctAnswer, option1, option2, option3, diff_id, cat_id)
values 
('What is the capital of Australia?', 'Canberra', 'Canberra', 'Sydney', 'Melbourne', 1, 1),
('Which river is the longest in the world?', 'Amazon', 'Nile', 'Amazon', 'Yangtze', 2, 1),
('Which country has the most islands in the world?', 'Sweden', 'Canada', 'Sweden', 'Indonesia', 3, 1),

('Who was the first President of the United States?', 'George Washington', 'George Washington', 'Thomas Jefferson', 'Abraham Lincoln', 1, 2),
('In what year did World War II end?', '1945', '1945', '1939', '1950', 2, 2),
('Which empire was ruled by Julius Caesar?', 'Roman Empire', 'Byzantine Empire', 'Roman Empire', 'Ottoman Empire', 3, 2),

('What is the powerhouse of the cell?', 'Mitochondria', 'Nucleus', 'Mitochondria', 'Ribosome', 1, 3),
('What is the process by which plants make their food?', 'Photosynthesis', 'Respiration', 'Photosynthesis', 'Osmosis', 2, 3),
('Which part of the human brain controls balance?', 'Cerebellum', 'Medulla', 'Cerebrum', 'Cerebellum', 3, 3),

('Who directed the movie Inception?', 'Christopher Nolan', 'Christopher Nolan', 'Steven Spielberg', 'James Cameron', 1, 4),
('Which movie won the Academy Award for Best Picture in 1994?', 'Forrest Gump', 'Forrest Gump', 'Pulp Fiction', 'The Shawshank Redemption', 2, 4),
('What is the name of the fictional kingdom in Black Panther?', 'Wakanda', 'Wakanda', 'Genovia', 'Zamunda', 3, 4);