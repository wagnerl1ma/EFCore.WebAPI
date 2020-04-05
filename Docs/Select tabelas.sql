USE HerosAppDb

SELECT * FROM Herois
SELECT * FROM Armas
SELECT * FROM Batalhas
SELECT * FROM HeroiBatalhas
SELECT * FROM IdentidadeSecreta


-- JOIN HEROIS COM ARMAS
SELECT * FROM Herois A
INNER JOIN Armas B ON A.Id = B.HeroiId


-- JOIN DE BATALHAS COM HEROI
SELECT A.*, B.*, C.* FROM Batalhas A
INNER JOIN HeroiBatalhas B ON A.Id = B.BatalhaId
INNER JOIN Herois C ON C.Id = B.HeroiId

