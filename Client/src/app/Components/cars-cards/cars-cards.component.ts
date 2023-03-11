import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CarsService } from 'src/app/Services/Cars/cars.service';
import { Car } from './../../Models/Car.model';

@Component({
  selector: 'cars-cards',
  templateUrl: './cars-cards.component.html',
  styleUrls: ['./cars-cards.component.scss'],
})
export class CarsCardsComponent implements OnInit {
  @Input() userAgent: string = 'Visitor';

  cars: Car[] = [];
  filteredCars: Car[] = [];

  //holds filter string
  filterValue: string = '';
  @Input() filterId: number = 0;

  constructor(
    private carsService: CarsService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  DeleteCar(id: number) {
    this.carsService.DeleteCar(id).subscribe({
      next: (response) => {
        location.reload();
      },
    });
  }

  ngOnInit(): void {
    // Read the filter parameter from the URL
    this.filterValue = this.route.snapshot.paramMap.get('filter') || 'All';

    this.carsService.GetAllCars().subscribe({
      next: (cars) => {
        this.cars = cars;
        this.loadCarImages();
        this.filterCars();
        console.log(cars);
      },
      error: (response) => console.error(response),
    });
  }

  private loadCarImages(): void {
    this.cars.forEach((car) => {
      this.carsService.GetImage(car.id).subscribe({
        next: (imageData) => (car.imageSrc = URL.createObjectURL(imageData)),
        error: (error) => console.error(error),
      });
    });
  }

  private filterCars(): void {
    if (this.filterValue !== '' && this.filterValue !== 'All') {
      this.filteredCars = this.cars.filter(
        (car) => car.category === this.filterValue
      );
    } else if (this.filterId !== 0) {
      this.filteredCars = this.cars.filter((car) => car.id === this.filterId);
    } else {
      this.filteredCars = this.cars;
    }
  }
}
