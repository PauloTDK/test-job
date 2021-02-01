export class User {
  id: number;
  nome: string;
  sobreNome: string;
  email: string;
  dataNascimento: Date;
  escolaridade: Escolaridade;
}

export enum Escolaridade {
  Infantil = 1,
  Fundamental = 2,
  Médio = 3,
  Superior = 4
}
