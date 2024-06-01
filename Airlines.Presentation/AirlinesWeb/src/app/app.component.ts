import { Component } from '@angular/core';

import {HubConnection, HubConnectionBuilder} from '@microsoft/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Airlines Web';
}
