import { Component } from '@angular/core';
import { User } from '../common/interfaces/user.interface';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private authService: AuthService) {
    
  }

  onLogoutClick(ev) {
    ev.stopPropagation();
    this.authService.logout().subscribe();

    return false;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
