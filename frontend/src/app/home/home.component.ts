import { Component, ViewChild, ElementRef } from "@angular/core";
import { CommonService } from "../service/common.service";


@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent {
  @ViewChild("addCustomer") addBtn!: ElementRef;

  constructor(
    private commonService: CommonService  ) {
    this.commonService.customerEdit_Observable.subscribe((res) => {
      this.addBtn.nativeElement.click();
    });
  }

  ngOnInit(): void {
  }

  onAddCustomerClick() {
    this.commonService.startAddMode();
  }

}
