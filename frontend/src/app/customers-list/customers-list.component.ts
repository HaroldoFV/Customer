import { Component, OnInit } from "@angular/core";
import { formatDistanceToNow } from "date-fns";
import { PaginatedCustomers, Customer } from "../models/customer";
import { CustomerService } from "../api/customer.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import { CommonService } from "../service/common.service";

@Component({
  selector: "app-customers-list",
  templateUrl: "./customers-list.component.html",
  styleUrls: ["./customers-list.component.css"],
  providers: [CustomerService],
})
export class CustomersListComponent implements OnInit {
  customers: Customer[] = [];
  expandedDetails: { [key: string]: boolean } = {};
  expandedAddresses: { [key: string]: boolean } = {};
  isEditingAddress: { [key: string]: boolean } = {};
  currentPage: number = 1;
  perPage: number = 6;
  totalCustomers: number = 0;
  errorMessage: string | null = null;
  successMessage: string = "";
  isAddingNewAddress: { [key: string]: boolean } = {};
  newAddress: any = {};


  constructor(
    private customerService: CustomerService,
    private commonService: CommonService,
    private snackBar: MatSnackBar
  ) { }

  async ngOnInit() {
    await this.loadCustomers();
    this.commonService.customerAdded_Observable.subscribe(() => {
      this.loadCustomers();
    });
  }

  loadCustomers(page: number = 1, event?: Event): void {
    if (event) {
      event.preventDefault();
    }

    this.customerService.getAll(page, this.perPage).subscribe(
      (response: PaginatedCustomers) => {
        this.customers = response.data;
        this.currentPage = response.meta.currentPage;
        this.totalCustomers = response.meta.total;
      },
      (error) => console.error(error)
    );
  }

  addNewAddress(customerId: string): void {
    this.isAddingNewAddress[customerId] = true;
    this.newAddress = {
      street: '',
      number: '',
      complement: '',
      neighborhood: '',
      city: '',
      state: '',
      zipCode: '',
    };
  }

  saveNewAddress(customerId: string): void {
    if (
      this.newAddress.street &&
      this.newAddress.number &&
      this.newAddress.city &&
      this.newAddress.state &&
      this.newAddress.zipCode
    ) {
      const addressModelInputs = [
        {
          street: this.newAddress.street,
          number: this.newAddress.number,
          complement: this.newAddress.complement,
          neighborhood: this.newAddress.neighborhood,
          city: this.newAddress.city,
          state: this.newAddress.state,
          zipCode: this.newAddress.zipCode,
        },
      ]; 

      this.customerService.addAddress(customerId, addressModelInputs).subscribe({
        next: (response) => {
          this.loadCustomers();

          this.showSuccessMessage('Address added successfully!');

          this.isAddingNewAddress[customerId] = false;
          this.newAddress = {};
        },
        error:
          (error) => this.handleErrorDelete(error)
      });
    }
  }

  isAddressValid(address: any): boolean {
    return (
      address.street &&
      address.number !== null && address.number !== undefined && address.number !== '' && address.number !== 'null' && address.number !== '0' &&
      address.city &&
      address.state &&
      address.zipCode
    );
  }

  cancelNewAddress(customerId: string): void {
    this.isAddingNewAddress[customerId] = false;
    this.newAddress = {};
  }

  nextPage(): void {
    if (this.currentPage * this.perPage < this.totalCustomers) {
      this.loadCustomers(this.currentPage + 1);
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.loadCustomers(this.currentPage - 1);
    }
  }

  totalPages(): number {
    return Math.ceil(this.totalCustomers / this.perPage);
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    const day = date.getDate().toString().padStart(2, "0");
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
  }

  formatDistanceToNow(dateString: string): string {
    return formatDistanceToNow(new Date(dateString), { addSuffix: true });
  }

  editCustomer(customer: Customer) {
    this.commonService.setCustomerToEdit(customer);
  }

  deleteAddress(customerId: string, address: any) {
    if (window.confirm("Are you sure you want to delete this address?")) {
      this.customerService.deleteAddresses(customerId, [address.id])
        .subscribe(
          (response) => {
            this.showSuccessMessage("Address deleted successfully");
            this.loadCustomers(this.currentPage);
          },
          (error) => this.handleErrorDelete(error)
        );
    }
  }

  deleteCustomer(id: string) {
    if (window.confirm("Are you sure you want to delete this customer?")) {
      this.customerService.delete(id).subscribe(
        () => {
          this.showSuccessMessage("Customer deleted successfully");
          this.loadCustomers(this.currentPage);
        },
        (error) => this.handleErrorDelete(error)
      );
    }
  }

  toggleDetailsExpansion(customerId: string | undefined): void {
    if (customerId) {
      this.expandedDetails[customerId] = !this.expandedDetails[customerId];
    }
  }

  toggleAddressExpansion(customerId: string | undefined): void {
    if (customerId) {
      this.expandedAddresses[customerId] = !this.expandedAddresses[customerId];
    }
  }

  handleError(error: any): void {
    const errorMessage = error?.error?.detail || "An unexpected error occurred";
    this.snackBar.open(errorMessage, "Close", {
      duration: 5000,
      verticalPosition: "bottom",
      horizontalPosition: "center",
      panelClass: ["error-snackbar"],
    });
  }

  handleErrorDelete(error: any): void {
    const errorMessage = error?.error?.detail || "An unexpected error occurred";
    this.errorMessage = errorMessage;
    setTimeout(() => {
      this.errorMessage = null;
    }, 5000);
  }

  private showSuccessMessage(message: string) {
    this.successMessage = message;
    setTimeout(() => {
      this.successMessage = "";
    }, 3000);
  }

  toggleEditAddress(customerId: string, addressIndex: number): void {
    const key = `${customerId}-${addressIndex}`;
    if (this.isEditingAddress[key]) {
      const customer = this.customers.find(c => c.id === customerId);
      if (customer) {
        const address = customer.addresses[addressIndex];
        this.customerService.updateAddress(customerId, address).subscribe(
          () => {
            this.showSuccessMessage("Address updated successfully");
            this.loadCustomers(this.currentPage);
          },
          (error) => this.handleErrorDelete(error)
        );
      }
    }

    this.isEditingAddress[key] = !this.isEditingAddress[key];
  }
}