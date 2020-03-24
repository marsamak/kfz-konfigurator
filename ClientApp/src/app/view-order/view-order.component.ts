import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'view-order',
  templateUrl: './view-order.component.html'
})
export class ViewOrderComponent {
  public order: Order;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Order>(baseUrl + 'vieworder/4').subscribe(result => {
      this.order = result;
    }, error => console.error(error));
  }
}