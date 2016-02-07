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
	color_code VARCHAR(255) DEFAULT '#E0E0E0',
	deleted BOOLEAN DEFAULT FALSE,
    last_updated TIMESTAMP
) AUTO_INCREMENT = 0;

CREATE TABLE bike
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,                    
	deleted BOOLEAN DEFAULT FALSE,
    last_updated TIMESTAMP
) AUTO_INCREMENT = 0;

CREATE TABLE route
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,                    
	distance FLOAT NOT NULL,
    notes TEXT,                    
	deleted BOOLEAN DEFAULT FALSE,
    last_updated TIMESTAMP
) AUTO_INCREMENT = 0;

CREATE TABLE ride
(
	id INT AUTO_INCREMENT PRIMARY KEY,
	
	route_id INT NOT NULL,
	rider_id INT NOT NULL,
	bike_id INT NOT NULL,
	payment_id INT NOT NULL,

	bike VARCHAR(255) NOT NULL,
	rider VARCHAR(255) NOT NULL,
	route VARCHAR(255) NOT NULL,
	
	ride_date DATETIME DEFAULT NULL, 
    notes TEXT,                    
	reward FLOAT DEFAULT 0,
	distance FLOAT DEFAULT 0,

    last_updated TIMESTAMP
) AUTO_INCREMENT = 0;

CREATE TABLE payment
(
	id INT AUTO_INCREMENT PRIMARY KEY,
	rider VARCHAR(255) NOT NULL,
	amount FLOAT NOT NULL,
	created_date DATETIME NOT NULL, 
	paid_date DATETIME,
    last_updated TIMESTAMP
) AUTO_INCREMENT = 0;

INSERT bike (name) VALUES ('Other');
INSERT route (name, distance, notes) VALUES ('Other', 0, 'Enter your own mileage');
