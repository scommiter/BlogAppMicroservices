import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FederatedComponent } from './federated.component';

describe('FederatedComponent', () => {
  let component: FederatedComponent;
  let fixture: ComponentFixture<FederatedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FederatedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FederatedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
