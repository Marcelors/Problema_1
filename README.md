# Problema_1

Cenário:

Quando fazemos o backup de arquivos, como imagens, músicas ou vídeos, um problema comum é saber se um determinado arquivo já foi armazenado ou não. A solução mais simples é fazer o backup de tudo. Isto é válido se você tem um espaço infinito para armazenar o seu backup. Não é uma solução tão boa se você possui um espaço limitado, dado que você pode acabar com a mesma foto da última viagem de férias em 5 locais diferentes dentro do seu backup.

Requisitos:

- O software deve ler e armazenar os arquivos em memória
- Deverá ser possível visualizar o conteúdo do arquivo.
- Tendo em vista que nosso espaço de armazenamento é escasso, não podemos nos dar o luxo de ter o mesmo conteúdo armazenado mais de uma vez.

Exemplo:

Arquivo1: Conteúdo exemplo X Arquivo2: Conteúdo exemplo Y

Arquivo3: Conteúdo exemplo X

O sistema deve armazenar apenas o conteúdo X e Y, mas deve permitir a consulta por qualquer arquivo.
