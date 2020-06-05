export class IRestaurant {
  id: string;
  name: string;
  email?: string;
  logoUrl?: string;
  description?: string;
  contact?: string;
  isActive?: boolean;
  dateTime?: string;
  sheetId?: string;
  // public List<MealDto> Meals { get; set; }
  // public List<DailyMenuDto> DailyMenus { get; set; }
}
