import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  optionsGroups: OptionGroup[];
  http: HttpClient;
  baseUrl: string;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    http.get<ConfigOption[]>(baseUrl + 'options').subscribe(result => {
      let groups = {};
      this.optionsGroups = [];
      result.forEach(option => {
        if (!groups[option.group])
          groups[option.group] = [option];
        else
          groups[option.group].push(option);
      });
      for (const key in groups) {
        this.optionsGroups.push({ 'group': key, 'options': groups[key], 'selected': groups[key][0] });
      }
    }, error => console.error(error));
  }

  totalPrice() {
    var price = 0;
    this.optionsGroups.forEach(group => { price += group.selected.price });
    return price;
  }
  sendOrder() {
    let order = new Order;
    order.totalPrice = this.totalPrice();
    this.optionsGroups.forEach(group => { order.optionIds.push(group.selected.id); });
    this.http.post<OrderResult>(this.baseUrl + "submitorder", order)
      .subscribe(orderResult => { window.alert('Link to your order: ' + this.baseUrl + "vieworder/" + orderResult.orderId); });
  }
}
class Order {
  optionIds: number[];
  totalPrice: number;
  constructor() { this.optionIds = []; }
}
class OrderResult {
  orderId: number;
}