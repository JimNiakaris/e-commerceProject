import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/service/shop.service';
import { Product } from '../../shared/models/product';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatSelectionList, MatActionList, MatListItem, MatListOption, MatSelectionListChange } from '@angular/material/list';

@Component({
  selector: 'app-shop',
  imports: [MatCard, ProductItemComponent, MatButton, MatIcon, MatMenu, MatSelectionList, MatMenuTrigger, MatActionList, MatListOption],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  baseUrl = "https://localhost:7126/api/"
  private shopService = inject(ShopService)
  private dialogeService = inject(MatDialog)
  products: Product[] = [];
  selectedBrands: string[] = [];
  selectedTypes: string[] = [];
  selectedSort: string = 'name';
  sortOptions = [{ name: 'Alphabetical', value: 'name' }, { name: 'Price Low-Hight', value: 'priceAsc' }, { name: 'Price High-Low', value: 'priceDesc' }]
  //products = signal<Product[]>([]); with NO zonechange detection

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProducts();

  }

  getProducts() {
    this.shopService.getProducts(this.selectedBrands,this.selectedTypes,this.selectedSort).subscribe({
      next: response => this.products = response.data, //next: response => this.products.set(response.data), with NO zonechange detection
      error: error => console.log(error)
    })
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.selectedSort = selectedOption.value;
      this.getProducts();
    }
  }

  openFiltersDialog() {
    const dialogRef = this.dialogeService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.selectedBrands,
        selectedTypes: this.selectedTypes
      }
    });
    dialogRef.afterClosed().subscribe({
      next: result => {
        if (result) {
          console.log(result);
          this.selectedBrands = result.selectedBrands;
          this.selectedTypes = result.selectedTypes;
          this.getProducts();
        }
      }
    })
  }
}
