DROP TABLE IF EXISTS rider;
DROP TABLE IF EXISTS bike;
DROP TABLE IF EXISTS route;
DROP TABLE IF EXISTS ride;
DROP TABLE IF EXISTS ride_archive;
DROP TABLE IF EXISTS payment;

CREATE TABLE rider
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
	rate INT DEFAULT 0,
    last_updated TIMESTAMP
);

CREATE TABLE bike
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,                    
    last_updated TIMESTAMP
);

CREATE TABLE route
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,                    
	distance FLOAT NOT NULL,
    notes TEXT,                    
    last_updated TIMESTAMP
) AUTO_INCREMENT = 0;

CREATE TABLE ride
(
	id INT AUTO_INCREMENT PRIMARY KEY,
	route_id INT NOT NULL,
	rider_id INT NOT NULL,
	bike_id INT,
	distance FLOAT DEFAULT 0,
	return_ride BOOLEAN DEFAULT FALSE,
	payable BOOLEAN DEFAULT FALSE,
	ride_date DATETIME DEFAULT NULL, 
    notes TEXT,                    
    last_updated TIMESTAMP
);

CREATE TABLE ride_archive
(
	id INT AUTO_INCREMENT PRIMARY KEY,
	route VARCHAR(255) NOT NULL,
	rider VARCHAR(255) NOT NULL,
	bike VARCHAR(255) NOT NULL,
	distance FLOAT NOT NULL,
	return_ride BOOLEAN NOT NULL,
	ride_date DATETIME NOT NULL, 
	archive_date DATETIME NOT NULL, 
	cash FLOAT NOT NULL,
    notes TEXT NOT NULL,                    
    last_updated TIMESTAMP
);

CREATE TABLE payment
(
	id INT AUTO_INCREMENT PRIMARY KEY,
	rider VARCHAR(255) NOT NULL,
	amount FLOAT NOT NULL,
	created_date DATETIME NOT NULL, 
	paid_date DATETIME,
    last_updated TIMESTAMP
);

INSERT route (name, distance, notes) VALUES ('Other', 0, 'Enter your own mileage');
