import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {

  // This Func Gets Urlpath, find matching link to router attribute, and then adds active to it
  ngOnInit(): void {
    const path: string = window.location.pathname;

    const link: HTMLAnchorElement | null = document.querySelector(
      `[routerLink="${path}"]`
    );

    if (link) {
      link.classList.add('active');
    }
  }
}
