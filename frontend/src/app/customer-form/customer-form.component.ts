import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { CustomerService } from "../api/customer.service";
import { Customer, Address } from "../models/customer";  // Certifique-se de importar Address
import { CommonService } from "../service/common.service";

@Component({
  selector: "app-customer-form",
  templateUrl: "./customer-form.component.html",
  styleUrls: ["./customer-form.component.css"],
})
export class CustomerFormComponent implements OnInit {
  customerId: string = "";
  customer: Customer = new Customer();
  errorMessage: string = "";
  isEditMode: boolean = false;
  successMessage: string = "";

  @ViewChild("closeBtn") closeBtn!: ElementRef;

  constructor(
    private customerService: CustomerService,
    private router: Router,
    private commonService: CommonService
  ) { 
    this.resetForm();
  }

  ngOnInit() {
    this.commonService.customerEdit_Observable.subscribe((res) => {
      if (this.commonService.customer_to_be_edited && this.commonService.customer_to_be_edited.id) {
        this.customer = this.commonService.customer_to_be_edited;
        this.isEditMode = true; 
        this.customer.birthDate = this.customer.birthDate.split('T')[0];
      } else { 
        this.resetForm();
        this.isEditMode = false;
      }

      if (!this.customer.addresses || this.customer.addresses.length === 0) {
        this.customer.addresses = [this.createEmptyAddress()];
      }

      this.errorMessage = "";


      this.commonService.customerAdded_Observable.subscribe(() => {
        this.startAddMode();
      });
    });
  }

  startAddMode() { 
    this.resetForm();
    this.isEditMode = false;
  }

  private resetForm(): void {
    this.customer = new Customer();
    this.customer.addresses = [this.createEmptyAddress()];
    this.isEditMode = false;
  }

  addCustomer() {
    if (this.customer.name && this.customer.genderType) {
      if (this.customer.id) {
        this.customerService.update(this.customer).subscribe(
          () => {
            this.showSuccessMessage("Customer updated successfully");
            this.afterSave();
          },
          (error) => this.handleError(error)
        );
      } else {
        this.customerService.insert(this.customer).subscribe(
          () => {
            this.showSuccessMessage("Customer added successfully");
            this.afterSave();
          },
          (error) => {
            this.handleError(error);
            this.resetForm();
          }
        );
      }
    } else {
      this.errorMessage = "Name and Gender are required";
    }
  }

  private createEmptyAddress(): Address {
    return {
      id: 0,
      street: "",
      number: "",
      complement: "",
      neighborhood: "",
      city: "",
      state: "",
      zipCode: "",
    };
  }

  addNewAddress() {
    this.customer.addresses.push(this.createEmptyAddress());
  }

  private afterSave() {
    if (this.closeBtn && this.closeBtn.nativeElement) {
      this.closeBtn.nativeElement.click();
    }
    this.isEditMode = false;
    this.resetForm();
    this.commonService.notifyCustomerAddition();
    this.errorMessage = "";

    this.router.navigate(['/customers']);
  }

  private handleError(error: any) {
    this.errorMessage = error?.error?.detail || "An unexpected error occurred";
  }

  cancel() {
    this.isEditMode = false;
    this.resetForm();
    this.navigateToCustomersList();
  }

  navigateToCustomersList() {
    this.router.navigate(["/customers"]);
  }

  addAddresses() {
    this.customer.addresses = [
      ...this.customer.addresses,
      this.createEmptyAddress()
    ];
  }

  removeAddress(index: number) {
    if (this.customer.addresses.length > 1) {
      this.customer.addresses.splice(index, 1);
    }
  }

  private showSuccessMessage(message: string) {
    this.successMessage = message;
    setTimeout(() => {
      this.successMessage = "";
    }, 3000);
  }

}