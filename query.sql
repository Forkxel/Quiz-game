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


-- Insert questions into Questions table
INSERT INTO Questions (questionText, correctAnswer, option1, option2, option3, diff_id, cat_id)
VALUES 
    -- Geography (Easy)
    ('What is the capital of France?', 'Paris', 'Paris', 'Berlin', 'Madrid', 1, 1),
    ('Which continent is the Amazon Rainforest located on?', 'South America', 'South America', 'Africa', 'Asia', 1, 1),
    ('What is the largest ocean in the world?', 'Pacific Ocean', 'Atlantic Ocean', 'Indian Ocean', 'Pacific Ocean', 1, 1),
    
    -- Geography (Medium)
    ('Which country has the highest number of active volcanoes?', 'Indonesia', 'Japan', 'Indonesia', 'Italy', 2, 1),
    ('What is the longest river in Asia?', 'Yangtze', 'Yangtze', 'Mekong', 'Ganges', 2, 1),
    ('What is the name of the smallest country in Europe?', 'Vatican City', 'Monaco', 'Vatican City', 'San Marino', 2, 1),

    -- Geography (Hard)
    ('What is the capital of Eritrea?', 'Asmara', 'Asmara', 'Kigali', 'Khartoum', 3, 1),
    ('Which desert is known as the world’s oldest desert?', 'Namib Desert', 'Sahara Desert', 'Namib Desert', 'Gobi Desert', 3, 1),
    ('Name the mountain range separating Europe and Asia.', 'Ural Mountains', 'Ural Mountains', 'Caucasus Mountains', 'Alps', 3, 1),

    -- History (Easy)
    ('Who discovered America in 1492?', 'Christopher Columbus', 'Christopher Columbus', 'Ferdinand Magellan', 'Vasco da Gama', 1, 2),
    ('What year marked the start of World War I?', '1914', '1914', '1939', '1929', 1, 2),
    ('Who was the first female Prime Minister of the UK?', 'Margaret Thatcher', 'Theresa May', 'Margaret Thatcher', 'Angela Merkel', 1, 2),

    -- History (Medium)
    ('What was the name of the ship on which the Pilgrims traveled to America?', 'Mayflower', 'Mayflower', 'Santa Maria', 'Endeavour', 2, 2),
    ('In which year did the Berlin Wall fall?', '1989', '1989', '1990', '1985', 2, 2),
    ('Who was the leader of the Soviet Union during World War II?', 'Joseph Stalin', 'Joseph Stalin', 'Vladimir Lenin', 'Nikita Khrushchev', 2, 2),

    -- History (Hard)
    ('What was the original name of Istanbul?', 'Byzantium', 'Constantinople', 'Byzantium', 'Alexandria', 3, 2),
    ('Name the treaty that ended the Napoleonic Wars.', 'Treaty of Paris', 'Treaty of Versailles', 'Treaty of Paris', 'Congress of Vienna', 3, 2),
    ('Which Chinese dynasty was the last to rule China?', 'Qing Dynasty', 'Ming Dynasty', 'Qing Dynasty', 'Han Dynasty', 3, 2),

    -- Biology (Easy)
    ('What is the basic unit of life?', 'Cell', 'Tissue', 'Organ', 'Cell', 1, 3),
    ('What is the process by which plants convert sunlight into food?', 'Photosynthesis', 'Respiration', 'Photosynthesis', 'Osmosis', 1, 3),
    ('What is the powerhouse of the cell?', 'Mitochondria', 'Nucleus', 'Mitochondria', 'Ribosome', 1, 3),

    -- Biology (Medium)
    ('What is the largest organ in the human body?', 'Skin', 'Heart', 'Skin', 'Liver', 2, 3),
    ('Which blood type is known as the universal donor?', 'O negative', 'O positive', 'O negative', 'AB negative', 2, 3),
    ('What is the primary function of red blood cells?', 'Transport oxygen', 'Fight infection', 'Transport oxygen', 'Produce energy', 2, 3),

    -- Biology (Hard)
    ('What is the name of the process by which DNA is copied?', 'Replication', 'Replication', 'Transcription', 'Translation', 3, 3),
    ('Which part of the brain regulates heartbeat and breathing?', 'Medulla', 'Cerebellum', 'Medulla', 'Cerebrum', 3, 3),
    ('What is the scientific name for humans?', 'Homo sapiens', 'Homo erectus', 'Homo sapiens', 'Homo habilis', 3, 3),

    -- Films (Easy)
    ('Who directed the movie Titanic?', 'James Cameron', 'James Cameron', 'Steven Spielberg', 'Christopher Nolan', 1, 4),
    ('What is the name of the wizarding school in Harry Potter?', 'Hogwarts', 'Durmstrang', 'Hogwarts', 'Beauxbatons', 1, 4),
    ('Who played the role of Iron Man in the Marvel Cinematic Universe?', 'Robert Downey Jr.', 'Chris Evans', 'Robert Downey Jr.', 'Mark Ruffalo', 1, 4),

    -- Films (Medium)
    ('Which movie won the Academy Award for Best Picture in 2003?', 'The Lord of the Rings: The Return of the King', 'Gladiator', 'The Lord of the Rings: The Return of the King', 'Chicago', 2, 4),
    ('What is the highest-grossing animated movie of all time?', 'Frozen II', 'Frozen', 'Frozen II', 'The Lion King', 2, 4),
    ('Which director is known for the films Pulp Fiction and Kill Bill?', 'Quentin Tarantino', 'Quentin Tarantino', 'Martin Scorsese', 'Ridley Scott', 2, 4),

    -- Films (Hard)
    ('Which film features the character "HAL 9000"?', '2001: A Space Odyssey', 'Blade Runner', '2001: A Space Odyssey', 'The Matrix', 3, 4),
    ('What is the name of the lead character in Schindler’s List?', 'Oskar Schindler', 'Itzhak Stern', 'Oskar Schindler', 'Amon Göth', 3, 4),
    ('Who composed the score for Star Wars?', 'John Williams', 'Hans Zimmer', 'John Williams', 'Ennio Morricone', 3, 4);

	-- Insert questions into Questions table
INSERT INTO Questions (questionText, correctAnswer, option1, option2, option3, diff_id, cat_id)
VALUES 
    -- Geography (Easy)
    ('What is the capital of France?', 'Paris', 'Paris', 'Berlin', 'Madrid', 1, 1),
    ('Which continent is the Amazon Rainforest located on?', 'South America', 'South America', 'Africa', 'Asia', 1, 1),
    ('What is the largest ocean in the world?', 'Pacific Ocean', 'Atlantic Ocean', 'Indian Ocean', 'Pacific Ocean', 1, 1),
    
    -- Geography (Medium)
    ('Which country has the highest number of active volcanoes?', 'Indonesia', 'Japan', 'Indonesia', 'Italy', 2, 1),
    ('What is the longest river in Asia?', 'Yangtze', 'Yangtze', 'Mekong', 'Ganges', 2, 1),
    ('What is the name of the smallest country in Europe?', 'Vatican City', 'Monaco', 'Vatican City', 'San Marino', 2, 1),

    -- Geography (Hard)
    ('What is the capital of Eritrea?', 'Asmara', 'Asmara', 'Kigali', 'Khartoum', 3, 1),
    ('Which desert is known as the world’s oldest desert?', 'Namib Desert', 'Sahara Desert', 'Namib Desert', 'Gobi Desert', 3, 1),
    ('Name the mountain range separating Europe and Asia.', 'Ural Mountains', 'Ural Mountains', 'Caucasus Mountains', 'Alps', 3, 1),

    -- History (Easy)
    ('Who discovered America in 1492?', 'Christopher Columbus', 'Christopher Columbus', 'Ferdinand Magellan', 'Vasco da Gama', 1, 2),
    ('What year marked the start of World War I?', '1914', '1914', '1939', '1929', 1, 2),
    ('Who was the first female Prime Minister of the UK?', 'Margaret Thatcher', 'Theresa May', 'Margaret Thatcher', 'Angela Merkel', 1, 2),

    -- History (Medium)
    ('What was the name of the ship on which the Pilgrims traveled to America?', 'Mayflower', 'Mayflower', 'Santa Maria', 'Endeavour', 2, 2),
    ('In which year did the Berlin Wall fall?', '1989', '1989', '1990', '1985', 2, 2),
    ('Who was the leader of the Soviet Union during World War II?', 'Joseph Stalin', 'Joseph Stalin', 'Vladimir Lenin', 'Nikita Khrushchev', 2, 2),

    -- History (Hard)
    ('What was the original name of Istanbul?', 'Byzantium', 'Constantinople', 'Byzantium', 'Alexandria', 3, 2),
    ('Name the treaty that ended the Napoleonic Wars.', 'Treaty of Paris', 'Treaty of Versailles', 'Treaty of Paris', 'Congress of Vienna', 3, 2),
    ('Which Chinese dynasty was the last to rule China?', 'Qing Dynasty', 'Ming Dynasty', 'Qing Dynasty', 'Han Dynasty', 3, 2),

    -- Biology (Easy)
    ('What is the basic unit of life?', 'Cell', 'Tissue', 'Organ', 'Cell', 1, 3),
    ('What is the process by which plants convert sunlight into food?', 'Photosynthesis', 'Respiration', 'Photosynthesis', 'Osmosis', 1, 3),
    ('What is the powerhouse of the cell?', 'Mitochondria', 'Nucleus', 'Mitochondria', 'Ribosome', 1, 3),

    -- Biology (Medium)
    ('What is the largest organ in the human body?', 'Skin', 'Heart', 'Skin', 'Liver', 2, 3),
    ('Which blood type is known as the universal donor?', 'O negative', 'O positive', 'O negative', 'AB negative', 2, 3),
    ('What is the primary function of red blood cells?', 'Transport oxygen', 'Fight infection', 'Transport oxygen', 'Produce energy', 2, 3),

    -- Biology (Hard)
    ('What is the name of the process by which DNA is copied?', 'Replication', 'Replication', 'Transcription', 'Translation', 3, 3),
    ('Which part of the brain regulates heartbeat and breathing?', 'Medulla', 'Cerebellum', 'Medulla', 'Cerebrum', 3, 3),
    ('What is the scientific name for humans?', 'Homo sapiens', 'Homo erectus', 'Homo sapiens', 'Homo habilis', 3, 3),

    -- Films (Easy)
    ('Who directed the movie Titanic?', 'James Cameron', 'James Cameron', 'Steven Spielberg', 'Christopher Nolan', 1, 4),
    ('What is the name of the wizarding school in Harry Potter?', 'Hogwarts', 'Durmstrang', 'Hogwarts', 'Beauxbatons', 1, 4),
    ('Who played the role of Iron Man in the Marvel Cinematic Universe?', 'Robert Downey Jr.', 'Chris Evans', 'Robert Downey Jr.', 'Mark Ruffalo', 1, 4),

    -- Films (Medium)
    ('Which movie won the Academy Award for Best Picture in 2003?', 'The Lord of the Rings: The Return of the King', 'Gladiator', 'The Lord of the Rings: The Return of the King', 'Chicago', 2, 4),
    ('What is the highest-grossing animated movie of all time?', 'Frozen II', 'Frozen', 'Frozen II', 'The Lion King', 2, 4),
    ('Which director is known for the films Pulp Fiction and Kill Bill?', 'Quentin Tarantino', 'Quentin Tarantino', 'Martin Scorsese', 'Ridley Scott', 2, 4),

    -- Films (Hard)
    ('Which film features the character "HAL 9000"?', '2001: A Space Odyssey', 'Blade Runner', '2001: A Space Odyssey', 'The Matrix', 3, 4),
    ('What is the name of the lead character in Schindler’s List?', 'Oskar Schindler', 'Itzhak Stern', 'Oskar Schindler', 'Amon Göth', 3, 4),
    ('Who composed the score for Star Wars?', 'John Williams', 'Hans Zimmer', 'John Williams', 'Ennio Morricone', 3, 4);
