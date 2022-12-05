import { Component, OnInit } from '@angular/core';
import { MatIconRegistry} from "@angular/material/icon";
import { DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  constructor(    private matIconRegistry: MatIconRegistry,
                  private domSanitizer: DomSanitizer
  ) {
    this.matIconRegistry.addSvgIcon(
      `dashboard`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/dashboard.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `forms`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/forms.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `logging`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/logging.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `placements`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/placements.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `messages`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/messages.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `notification-idle`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/notification_idle.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `notification-new`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/notification_new.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `settings`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/settings.svg")
    );

  }

  ngOnInit(): void {
  }

}
