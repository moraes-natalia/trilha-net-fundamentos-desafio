# Sistema de Estacionamento

Este projeto faz parte dos desafios práticos do **Bootcamp WEX – End to End Engineering**, uma iniciativa conjunta entre a **WEX**, empresa global de tecnologia financeira, e a plataforma de ensino **DIO (Digital Innovation One)**. O objetivo é aplicar, na prática, conceitos de desenvolvimento de software por meio da construção de um sistema simples de controle de estacionamento.

**🔗 Exercício Original:** [https://github.com/digitalinnovationone/trilha-net-fundamentos-desafio](https://github.com/digitalinnovationone/trilha-net-fundamentos-desafio)

**Proposta do Desafio:**
1. Cadastrar veículo
2. Remover veículo
3. Listar veículos
4. Encerrar

# Documentação - Sistema de Estacionamento Implementado

## Visão Geral
Sistema desenvolvido em C# .NET para gerenciamento de estacionamento com validação de placas brasileiras (formato antigo e Mercosul). O sistema implementa as 4 funcionalidades propostas no desafio original (cadastrar, remover, listar veículos e encerrar), além de melhorias adicionais como validação avançada de placas, prevenção de duplicatas e formatação automática.

## Estrutura da Classe `Estacionamento`

### Propriedades Privadas
- `decimal precoInicial`: Preço base cobrado para estacionar
- `decimal precoPorHora`: Valor cobrado por hora de permanência
- `List<string> veiculos`: Lista que armazena as placas dos veículos estacionados

### Construtor
```csharp
public Estacionamento(decimal precoInicial, decimal precoPorHora)
```
Inicializa o sistema com os valores de preço inicial e preço por hora.

## Métodos Implementados

### 1. `ValidarPlacaBrasileira(string placa)` - **PRIVADO**
**Funcionalidade:** Valida se a placa está no formato brasileiro válido.

**Características:**
- Aceita formato antigo: `ABC1234` ou `ABC-1234` (3 letras + 4 números)
- Aceita formato Mercosul: `ABC1D23` (3 letras + 1 número + 1 letra + 2 números)
- Remove espaços e hífens para padronização
- Utiliza expressões regulares (Regex) para validação
- Converte para maiúsculas automaticamente

**Retorno:** `bool` - true se válida, false se inválida

---

### 2. `FormatarPlaca(string placa)` - **PRIVADO**
**Funcionalidade:** Formata a placa no padrão brasileiro correto.

**Características:**
- Formato antigo: adiciona hífen (`ABC-1234`)
- Formato Mercosul: mantém sem hífen (`ABC1D23`)
- Remove caracteres extras (espaços, hífens desnecessários)
- Converte para maiúsculas

**Retorno:** `string` - placa formatada

---

### 3. `AdicionarVeiculo()` - **PÚBLICO**
**Funcionalidade:** Adiciona um novo veículo ao estacionamento.

**Fluxo de execução:**
1. Solicita placa do usuário via console
2. Exibe formatos aceitos como orientação
3. Valida placa usando `ValidarPlacaBrasileira()`
4. Formata placa usando `FormatarPlaca()`
5. Verifica se placa já não está cadastrada (evita duplicatas)
6. Adiciona à lista `veiculos`
7. Exibe confirmação ou mensagem de erro

**Validações:**
- ✅ Formato de placa brasileiro
- ✅ Placa não duplicada
- ✅ Entrada não vazia

---

### 4. `RemoverVeiculo()` - **PÚBLICO**
**Funcionalidade:** Remove veículo do estacionamento e calcula valor a pagar.

**Fluxo de execução:**
1. Solicita placa do usuário
2. Valida formato da placa
3. Verifica se veículo está no estacionamento
4. Solicita quantidade de horas
5. Calcula valor total: `precoInicial + (precoPorHora × horas)`
6. Remove veículo da lista
7. Exibe valor total formatado

**Cálculo:** `valorTotal = precoInicial + (precoPorHora * horas)`

**Validações:**
- ✅ Formato de placa brasileiro
- ✅ Veículo existe no estacionamento
- ✅ Horas é número inteiro não negativo

---

### 5. `ListarVeiculos()` - **PÚBLICO**
**Funcionalidade:** Lista todos os veículos atualmente estacionados.

**Características:**
- Verifica se existem veículos na lista
- Numera os veículos (1º, 2º, 3º...)
- Exibe mensagem apropriada quando lista vazia
- Usa loop `for` para percorrer a lista

**Saída:**
- Se há veículos: Lista numerada
- Se vazia: "Não há veículos estacionados."

---

## Menu Principal (Program.cs)

### Funcionalidades do Menu:
1. **Cadastrar veículo** - Chama `AdicionarVeiculo()`
2. **Remover veículo** - Chama `RemoverVeiculo()`
3. **Listar veículos** - Chama `ListarVeiculos()`
4. **Encerrar** - Finaliza aplicação

### Características:
- Loop infinito até escolha da opção "4"
- Limpa tela a cada iteração (`Console.Clear()`)
- Pausa para leitura após cada operação
- Tratamento de opções inválidas

---

## Validações e Tratamentos de Erro

### Placas Brasileiras:
- **Formato Antigo:** `^[A-Z]{3}[0-9]{4}$`
- **Formato Mercosul:** `^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$`

### Exemplos Válidos:
- ✅ `ABC-1234` (antigo com hífen)
- ✅ `ABC1234` (antigo sem hífen)
- ✅ `ABC1D23` (Mercosul)
- ✅ `abc1d23` (aceita minúsculas)

### Exemplos Inválidos:
- ❌ `AB1234` (poucas letras)
- ❌ `ABCD123` (muitas letras)
- ❌ `ABC12345` (muitos números)
- ❌ `123ABCD` (ordem incorreta)

### Outras Validações:
- Horas deve ser número inteiro não negativo
- Não permite placas duplicadas
- Não permite campos vazios

---

## Bibliotecas Utilizadas

### System.Text.RegularExpressions
- `Regex.IsMatch()`: Validação de padrões de placa

### System.Linq
- `Any()`: Verificação de existência na lista
- `First()`: Localização de elementos

### System.Collections.Generic
- `List<string>`: Armazenamento das placas

---

## Formatação de Dados

### Valores Monetários:
- Formato: `R$ XX,XX`
- Precisão: 2 casas decimais
- Uso: `{valorTotal:F2}`

### Placas:
- Sempre em maiúsculas
- Formato antigo com hífen
- Formato Mercosul sem hífen

---

## Melhorias Implementadas

### Além dos Requisitos Básicos:
1. **Validação completa de placas brasileiras**
2. **Prevenção de duplicatas**
3. **Formatação padronizada**
4. **Mensagens informativas**
5. **Tratamento robusto de erros**
6. **Interface amigável com exemplos**
7. **Suporte aos dois formatos de placa (antigo e Mercosul)**

### Experiência do Usuário:
- Mensagens claras de orientação
- Exemplos de formato aceito
- Feedback imediato sobre erros
- Confirmações de operações realizadas

---

**Bootcamp WEX - End to End Engineering - [DIO](https://web.dio.me/)**  
Desenvolvido por [Natalia Moraes](https://www.linkedin.com/in/moraesnatalia/)