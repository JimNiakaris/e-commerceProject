import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/service/shop.service';
import { Product } from '../../shared/models/product';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from "./product-item/product-item.component";

@Component({
  selector: 'app-shop',
  imports: [MatCard, ProductItemComponent],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit{
  baseUrl = "https://localhost:7126/api/"
  private shopService = inject(ShopService)
  products: Product[] = [];
  //products = signal<Product[]>([]); with NO zonechange detection

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop(){
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.shopService.getProducts().subscribe({
    next: response => this.products = response.data, //next: response => this.products.set(response.data), with NO zonechange detection
    error: error => console.log(error) 
   })
  }
}
