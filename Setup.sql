USE amazenapi;

-- DROP TABLE products;

CREATE TABLE products 
(
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    price INT NOT NULL,
    description VARCHAR(255),
    creatorId VARCHAR(255),

    PRIMARY KEY (id),

    FOREIGN KEY (creatorId)
    REFERENCES profiles (id)
    ON DELETE CASCADE
)

-- CREATE TABLE profiles
-- (
--     id VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     picture VARCHAR(255) NOT NULL,

--     PRIMARY KEY (id)
-- )