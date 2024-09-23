export interface Address {
  id: number,
  street: string;
  number: string;
  complement: string;
  neighborhood: string;
  city: string;
  state: string;
  zipCode: string;
}
 
export class Customer {
  id?: string; 
  name: string = '';  
  birthDate: string = '';  
  genderType: number = 0;  
  addresses: Address[] = [];  
}

export interface PaginationMeta {
  currentPage: number;
  perPage: number;
  total: number;
}

export interface PaginatedCustomers {
  meta: PaginationMeta;
  data: Customer[];
}
