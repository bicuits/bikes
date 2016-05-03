use bikes;

ALTER TABLE rider
	ADD COLUMN defaults TEXT;
    
UPDATE rider SET defaults = '{}';