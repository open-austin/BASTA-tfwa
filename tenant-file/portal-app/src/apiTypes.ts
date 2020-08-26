export interface Tenant {
  name: string;
  lastUpdated: string;
  imageIds: string[];
  property: Property;
}

export interface Property {
  name: string;
}
