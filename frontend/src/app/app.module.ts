import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ReactiveFormsModule } from "@angular/forms";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { CustomerFormComponent } from "./customer-form/customer-form.component"; 
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { CustomersListComponent } from "./customers-list/customers-list.component";
import { HomeComponent } from "./home/home.component";
import { MatIconModule } from "@angular/material/icon";
import { CommonService } from "./service/common.service";

@NgModule({
  declarations: [
    AppComponent,
    CustomersListComponent,
    CustomerFormComponent,
    HomeComponent, 
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatSnackBarModule, 
    MatIconModule,
    FormsModule
  ],
  providers: [ 
    CommonService
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
