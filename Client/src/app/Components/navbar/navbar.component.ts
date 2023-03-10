import { FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { CarsService } from 'src/app/Services/Cars/cars.service';
import { Router } from '@angular/router';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  searchQuery = new FormControl('');
  searchResult: string | undefined;
  searchError: boolean | undefined;

  constructor(private carsService: CarsService,private router:Router) {}

  // This Func Gets Urlpath, find matching link to router attribute, and then adds active to it
  ngOnInit(): void {
    const path: string = window.location.pathname;

    const link: HTMLAnchorElement | null = document.querySelector(
      `[routerLink="${path}"]`
    );

    if (link) {
      link.classList.add('active');
    }

    this.searchQuery.valueChanges.subscribe((value) => {
      console.log(value); // prints the current value of the input field
    });
    
  }

  search() {
    this.carsService.GetCarIdUsingName(this.searchQuery.value!).subscribe({
      next: (carId) => {
        this.searchError = undefined;
        this.router.navigate(['/Cars/id/'+carId]);
      },
      error: (err) => {
        this.searchError = err.message;
        this.searchResult = 'Not found';
        console.log(err);
        setTimeout(() => {
          this.searchError = false;
        }, 2000);
      },
    });
  }
}