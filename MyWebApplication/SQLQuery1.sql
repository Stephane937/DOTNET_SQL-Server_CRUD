CREATE TABLE velo (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    station VARCHAR (100) NOT NULL,
    nbreBornettesLibres INT NOT NULL,
    velosDispo INT NOT NULL,
    veloManuelle INT NOT NULL,
    veloElectriques INT NOT NULL,
    bornePaiement VARCHAR (100) NOT NULL
);

INSERT INTO velo (station, nbreBornettesLibres, velosDispo, veloManuelle,veloElectriques,bornePaiement)
VALUES
('Paris', 65,60,5,55,'OUI'),
('montigny', 65,60,5,55,'NON'),
('evry', 65,60,5,55,'NON');
