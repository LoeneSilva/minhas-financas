﻿DROP TABLE financas;
CREATE TABLE financas(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	valor DECIMAL(8,2),
	tipo VARCHAR(100),
	descricao VARCHAR(100),
	status VARCHAR(100)
);
SELECT * from financas;
