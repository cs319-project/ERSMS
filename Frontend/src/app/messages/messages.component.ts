import { Component} from '@angular/core';

@Component({
    selector: 'app-messages',
    templateUrl: './messages.component.html',
    styleUrls: ['./messages.component.css']
  })

export class MessagesComponent{
    numbers = Array(15).fill(4)
}
