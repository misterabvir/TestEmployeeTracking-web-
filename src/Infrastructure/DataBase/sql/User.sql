CREATE TABLE Users(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Email VARCHAR(255) NOT NULL,
	Password VARCHAR(255) NOT NULL,
	Salt uniqueidentifier NOT NULL
);