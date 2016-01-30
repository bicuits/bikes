DROP TABLE IF EXISTS rider;
DROP TABLE IF EXISTS bike;
DROP TABLE IF EXISTS route;
DROP TABLE IF EXISTS ride;

CREATE TABLE rider
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
	rate INT DEFAULT 0,
	deleted BOOLEAN DEFAULT FALSE,
    last_updated TIMESTAMP
);

CREATE TABLE bike
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,                    
	deleted BOOLEAN DEFAULT FALSE,
    last_updated TIMESTAMP
);

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
	bike_id INT,
	distance FLOAT NOT NULL,
	ride_date DATETIME DEFAULT NULL, 
    notes TEXT,                    
    last_updated TIMESTAMP
);

INSERT route (name, distance, notes) VALUES ('Other', 0, 'Enter your own mileage');
