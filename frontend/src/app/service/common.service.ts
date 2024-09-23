import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { Customer } from "../models/customer";

@Injectable()
export class CommonService {
  public customerAdded_Observable = new Subject();
  public customerEdit_Observable = new Subject();
  public customer_to_be_edited;

  constructor() {
    this.customer_to_be_edited = new Customer();
  }

  notifyCustomerEdit() {
    this.customerEdit_Observable.next();
  }

  setCustomerToEdit(customer: Customer) {
    this.customer_to_be_edited = customer;
    this.notifyCustomerEdit();
  }

  notifyCustomerAddition() {
    this.customerAdded_Observable.next();
  }

  startAddMode() {
    this.customerAdded_Observable.next();
  }
}
