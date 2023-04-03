CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8mb4 COMMENT '';

CREATE TABLE
    keeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(1000) NOT NULL DEFAULT 'Empty',
        img VARCHAR(255) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1511914678378-2906b1f69dcf?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&h=400&w=687&q=80',
        views INT NOT NULL DEFAULT 0,
        creatorId VARCHAR(255) NOT NULL,
        kept VARCHAR(255) NOT NULL,
        Foreign Key (kept) REFERENCES vaultKeep(id)
    ) default charset utf8mb4 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(1000) NOT NULL DEFAULT 'Empty',
        img VARCHAR(255) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1511914678378-2906b1f69dcf?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&h=400&w=687&q=80',
        isPrivate BOOLEAN NOT NULL DEFAULT false,
        FOREIGN KEY(creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultKeep(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        vaultId int NOT NULL,
        keepId int NOT NULL,
        Foreign Key (vaultId) REFERENCES vaults(id),
        Foreign Key (keeptId) REFERENCES keeps(id),
        FOREIGN KEY(creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';