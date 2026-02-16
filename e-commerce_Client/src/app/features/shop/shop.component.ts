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
import { ShopParams } from '../../shared/models/shopParams';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';

@Component({
  selector: 'app-shop',
  imports: [MatCard, ProductItemComponent, MatButton, MatIcon, MatMenu, MatSelectionList, MatMenuTrigger, MatListOption, MatPaginator],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  baseUrl = "https://localhost:7126/api/"
  private shopService = inject(ShopService)
  private dialogeService = inject(MatDialog)
  products?: Pagination<Product>;

  sortOptions = [{ name: 'Alphabetical', value: 'name' }, { name: 'Price Low-Hight', value: 'priceAsc' }, { name: 'Price High-Low', value: 'priceDesc' }]
  //products = signal<Product[]>([]); with NO zonechange detection
  shopParams = new ShopParams();
  pageSizeOptions = [5,10,15,20]

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProducts();

  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response, //next: response => this.products.set(response.data), with NO zonechange detection
      error: error => console.log(error)
    })
  }

  handlehandlePageEvent(event: PageEvent){
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response=> this.products = response,
      error: error => console.error(error),
      
    })
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.getProducts();
    }
  }

  openFiltersDialog() {
    const dialogRef = this.dialogeService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types
      }
    });
    dialogRef.afterClosed().subscribe({
      next: result => {
        if (result) {
          console.log(result);
          this.shopParams.brands = result.selectedBrands;
          this.shopParams.types = result.selectedTypes;
          this.getProducts();
        }
      }
    })
  }
}
