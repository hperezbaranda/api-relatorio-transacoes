# Solucao Web Api
Contem a API e o UnitTeste.

Para a implementação do desafio foi usado C# com .Net Core 2.1 e o SGBD MongoDB.

O API faz o relatorio das transações filtrando por cnpj do lojista, data, bandeira ou adquirente... permitendo que estes sejam compostos, usando a vírgula (,) como separador. 

## Exemplos de acesso aos dados : 
- Procurando as transações que foram feitas no dia 25 de Julho do 2018: ***http://dir:port/api/trans/data/2018-07-25***.

*NOTA* Este é a única pesquisa que nao pode-se usa a vírgula para fazer-lo composto.
- Procurando as transações que foram feitas pela loja "123": ***http://dir:port/api/trans/cnpj/123***.
- Pocurando as transações que foram feitas pela loja "123" e "890": ***http://dir:port/api/trans/cnpj/123,890***.
- Pocurando as transações que foram feitas pelo adquirente Stone: ***http://dir:port/api/trans/acquirer/Stone***.
- Pocurando as transações que foram feitas pelo adquirente Stone e Getnet: ***http://dir:port/api/trans/acquirer/Stone,Getnet***.
- Pocurando as transações que foram feitas usando um cartão de bandeira Visa: ***http://dir:port/api/trans/brandname/Visa***.
- Pocurando as transações que foram feitas usando um cartão de bandeira Visa e Electron: ***http://dir:port/api/trans/brandname/Visa,Electron***.
- Pocurando as transações que foram feitas nos últimos x días: ***http://dir:port/api/trans/last/4***.

Mas as filtragem também podem ser composto por parametros de tipos diferentes, para isso a API precisará que os parametros sejam pasado por declaração de variaveis:
- Pocurando as transações que foram feitas usando um cartão de bandeira Visa nos últimos 5 dias: ***http://dir:port/api/trans/?brandname="Visa"&days=5***.
*IMPORTANTE!* A requisição para uma data especifica e para x días são filtros excluyentes, portanto sempre vai se procurar a variavel *data* se não existir então vai se validar a variavel *day*.
