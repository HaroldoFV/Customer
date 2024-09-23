import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { PaginatedCustomers, Customer, Address } from "../models/customer";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class CustomerService {
  apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAll(page: number = 1, perPage: number = 15): Observable<PaginatedCustomers> {
    return this.http.get<PaginatedCustomers>(
      `${this.apiUrl}/api/customers?page=${page}&per_page=${perPage}&sort=createdat&dir=1`
    );
  }

  getById(id: string) {
    return this.http
      .get<{ data: Customer }>(`${this.apiUrl}/api/customers/${id}`)
      .pipe(map((response) => response.data));
  }

  insert(customer: Customer) { 
    return this.http.post(`${this.apiUrl}/api/customers/`, customer);
  }

  update(customer: Customer) {
    return this.http.put(`${this.apiUrl}/api/customers/${customer.id}`, customer);
  }

  delete(id: string) {
    return this.http.delete(`${this.apiUrl}/api/customers/${id}`);
  }

  deleteAddresses(customerId: string, addressesIds: number[]) {
    return this.http.request('DELETE', `${this.apiUrl}/api/customers/${customerId}/addresses`,
      {
        body: addressesIds,
        headers: {
          'Content-Type': 'application/json'
        }
      });
  }

  updateAddress(customerId: string, address: Address) {
    return this.http.put(`${this.apiUrl}/api/customers/${customerId}/addresses/${address.id}`, address);
  }

  addAddress(customerId: string, addressModelInputs: any[]): Observable<any> {
    const url = `${this.apiUrl}/api/customers/${customerId}/addresses`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(url, { addressModelInputs }, { headers });
  }

}
