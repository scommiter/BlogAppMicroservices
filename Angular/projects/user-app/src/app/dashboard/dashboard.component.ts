import { Component, OnInit } from '@angular/core';
import { User, } from 'oidc-client-ts';
import { AuthLibService } from 'auth-lib';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit{
  private _user!: User;

  constructor(private authSerivce: AuthLibService, private route: ActivatedRoute) {
  }
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const value = history.state.key;
      console.log("TOKEN", value);
    });

    // Another way to receive data from host-app
    // this.route.data.subscribe(data => {
    //   const inputText = data['inputText'];
    //   console.log('Received data:', inputText);
    // });
  }
}
