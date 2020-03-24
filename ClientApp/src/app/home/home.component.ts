import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  optionsGroups: OptionGroup[];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
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
    var selectedOptionsString = "";
    this.optionsGroups.forEach(group => { selectedOptionsString += group.selected.id + ";" })
    window.alert(selectedOptionsString);
  }
}
