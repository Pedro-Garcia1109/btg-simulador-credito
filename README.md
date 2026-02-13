Simulador de Crédito – BTG Pactual Descrição

Este projeto foi desenvolvido como parte de um teste técnico com o objetivo de implementar um simulador de crédito utilizando o ecossistema .NET.

A aplicação permite que o usuário informe os dados necessários para uma simulação de crédito e retorne o detalhamento das parcelas calculadas com base em juros compostos.

Tecnologias Utilizadas

.NET 8

ASP.NET Core

Razor Pages

Entity Framework Core (InMemory)

Swagger (documentação da API)

Funcionalidades

A aplicação permite:

Informar valor total do crédito

Informar data de início e término do contrato

Informar taxa de juros anual

Selecionar frequência de pagamento (mensal, trimestral, semestral ou anual)

Calcula as parcelas utilizando juros compostos

Converte corretamente a taxa anual para a taxa equivalente do período

Ajusta automaticamente a data da parcela para o próximo dia útil caso caia em sábado ou domingo

Exibe:

Data da parcela

Valor do principal

Valor total com juros

Armazena as simulações realizadas em banco de dados em memória

Regra de Cálculo

A taxa anual é convertida para taxa do período utilizando a fórmula:

(1 + taxaAnual)^(1 / períodosPorAno) - 1

O valor futuro da parcela é calculado por:

VF = VP * (1 + i)^n

VP = valor principal da parcela

i = taxa do período

n = número do período

Foi utilizado Entity Framework Core com banco em memória (UseInMemoryDatabase) conforme solicitado no enunciado.

As simulações ficam armazenadas durante o tempo de execução da aplicação.

Como Executar o Projeto Pré-requisitos

.NET 8 SDK instalado

Passos

Clonar o repositório

Acessar a pasta do projeto

Executar: (pode ser necessário baixar a versão mais recente do entity framework também!)

dotnet run

Acessar no navegador:

http://localhost:???

Para acessar a documentação da API:

http://localhost:????/swagger

Observações

O sistema considera apenas sábado e domingo como dias não úteis.

Não foram considerados feriados nacionais.

O projeto foi estruturado separando responsabilidades entre camadas de aplicação, infraestrutura e apresentação.

Possíveis Melhorias Futuras

Implementação de testes unitários

Validações adicionais no backend

Persistência em banco relacional

Autenticação e controle de acesso

Consideração de feriados no cálculo de datas úteis
