   M S B A M L   `   `   `  ����A  =Szkola, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Szkola.ViewSzkola.View  5      $    Szkola.View.WszystkieKlasyView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Szkola.View     5        � 	Resources  5	       �� Source�� 5
   
   $! StyleZakladekResources.xaml?�6   %    .��5      �� .��5   
   �    GridStandard#��[  6    � RowDefinitions 5      �� 5      $��70 �6    �� 5      $��* �6    �� 5      $��3* �6    .+�5      �  � ColumnDefinitions 5      �� 5      $��3* �6'   �� 5      $��4* �6'   �� 5      $��1* �6'   5      � 5      $��3��6   $��Center=�6     5      �� 5      $	��11* �6'   �� 5      $��1* �6'   �� 5      $	��10* �6'    5!      �� 5"      $��30 �6$   �� 5#      $	��330 �6$   .+�5%      &   System.Windows.Controls.DataGrid  $
��Left=�6      AutoGenerateColumns$ false��66   $��1��6R   ��6_   �� *List+ �� SelectedItem 6|   ��  �� Path$ WybranaKlasa� �� Mode$ TwoWay=�   Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Klasa��6-   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 40 6<   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� $ Klasa�  5(      $	 
Nauczyciel��6-   $	
 150 6A    6M   �� $ 
Nauczyciel�  5)      $	 Ilość uczniów��6-   $
 90 6D    6O   �� $ IloscUczniow�  5*      $	 
Numer sali��6-   $
 80 6A    6L   �� $ 	NumerSali�5,      �� 5.      $��Wyświetl uczniów��6   $��0��64   $��2��6A     ButtonFillStandard#��[ 6Q   ��6}   �� *ShowStudensCommand+  5/      $
��Left=�6   $ false��66   $��1��6R   $��2��6_   ��6o   �� *UczniowieZKlasyList+ 6�   �� $ WybranaKlasa�$ TwoWay=� 50        51      $
	 Imie��6-   $	
 130 6;    6G   �� $
 Imie�  52      $	 Nazwisko��6-   $	
 150 6?    6K   �� $ Nazwisko�  53      $	 Pesel��6-   $
 90 6<    6G   �� $ Pesel�  54      $	 Średnia ocen��6-   $
 50 6C    6N   �� $ SredniaOcen�56      58      59      5:      5;      