import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-not-authoarized-page',
  templateUrl: './not-authoarized-page.component.html',
  styleUrls: ['./not-authoarized-page.component.scss']
})
export class NotAuthoarizedPageComponent implements OnInit {

  constructor(private router:Router){}

  ngOnInit() {
    setTimeout(() => {
      this.router.navigate(['Login']);
    }, 3000);
}
}