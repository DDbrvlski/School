   M S B A M L   `   `   `  ����A  =Szkola, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Szkola.ViewSzkola.View  5      (    "Szkola.View.WszyscyUzytkownicyView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Szkola.View     5        � 	Resources  5	       �� Source�� 5
   
   $! StyleZakladekResources.xaml?�6   %    .��5      �� .��5   
   �    GridStandard#��[  6    � RowDefinitions 5      �� 5      $��70 �6    �� 5      $��* �6    �� 5      $��3* �6     � ColumnDefinitions 5      �� 5      $��3* �6#   �� 5      $��4* �6#   �� 5      $��1* �6#   .+�5      � $��2��6   $��3��6    $��Bottom=�64   $��Center=�6O    5      �� 5      $��30 �6$   �� 5      $	��300 �6$   .+�5      �� $��Wyniki wyszukiwania��6   $��0��66   &   System.Windows.Controls.DataGrid  5      $��Center=�6      AutoGenerateColumns$ false��68   $��1��6T   ��6a   �� *List+ �� SelectedItem 6~   ��  �� Path$ WybranyStudent� �� Mode$ TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6-   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 40 69      
IsReadOnly$
 True��6j   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� $ IdUzytkownik�6j     5!      $	 Imię��6-   $	
 140 6;   $
 True��6e    6G   �� $
 Imie�6e     5"      $	 Nazwisko��6-   $	
 140 6?   $
 True��6m    6K   �� $ Nazwisko�6m     5#      $	 Klasa��6-   $
 80 6<   $
 True��6f    6G   �� $ Klasa�6f     5$      $	 Płeć��6-   $	
 120 6;   $
 True��6e    6G   �� $
 Plec�6e     5%      $	 Status��6-   $	
 125 6=   $
 True��6i    6I   �� $ Status�6i     5&      $	 Pesel��6-   $	
 140 6<   $
 True��6g    6H   �� $ Pesel�6g     5'      $	 Telefon��6-   $	
 140 6>   $
 True��6k    6J   �� $ Telefon�6k   5)      5*      5+      5,      5-      